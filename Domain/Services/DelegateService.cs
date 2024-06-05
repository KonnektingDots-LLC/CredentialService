using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Exceptions;
using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace cred_system_back_end_app.Domain.Services
{
    public class DelegateService : IDelegateService
    {
        private readonly DbContextEntity _context;
        private readonly IProviderService _providerService;
        private readonly IProviderDelegatesRepository _providerDelegatesRepository;
        private readonly IDelegateRepository _delegateRepository;

        public DelegateService(IProviderService providerService, IProviderDelegatesRepository providerDelegatesRepository, DbContextEntity context, IDelegateRepository delegateRepository)
        {
            _context = context;
            _providerService = providerService;
            _providerDelegatesRepository = providerDelegatesRepository;
            _delegateRepository = delegateRepository;
        }

        /// <summary>
        /// Get provider delegates by provider id
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProviderDelegateEntity>> GetProviderDelegatesByProviderId(int providerId)
        {
            return await _providerDelegatesRepository.SearchByProviderId(providerId);
        }

        /// <summary>
        /// Update delegate is active status flag.
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="delegateId"></param>
        /// <param name="providerEmail"></param>
        /// <returns></returns>
        /// <exception cref="GenericDelegateException"></exception>
        public async Task<(ProviderEntity, DelegateEntity)> UpdateProviderDelegateStatusAsync(bool isActive, int delegateId, string providerEmail)
        {
            var provider = await _providerService.GetProviderByEmail(providerEmail);
            var providerDelegate = await _providerDelegatesRepository.GetByDelegateIdandProviderIdAsync(delegateId, provider.Id)
                ?? throw new GenericDelegateException($"No delegate [${delegateId}] associated with provider [${provider.Id}] found.");

            await _providerDelegatesRepository.UpdateIsActiveByDelegateIdAndProviderIdAsync(delegateId, provider.Id, isActive);
            return (provider, providerDelegate.Delegate);
        }

        /// <summary>
        /// Create provider delegate relation. If delegate does not exixts create a placeholder. Delegate must complete registration.
        /// </summary>
        /// <param name="providerId"></param>
        /// <param name="delegateEmail"></param>
        /// <returns></returns>
        public async Task<ProviderEntity?> CreateProviderDelegateRelationAsync(int providerId, string delegateEmail)
        {
            var provider = await _providerService.GetProviderById(providerId);
            var delegateEntity = await _delegateRepository.GetByEmailAsync(delegateEmail);

            if (delegateEntity == null)
            {
                delegateEntity = new DelegateEntity
                {
                    Email = delegateEmail,
                    DelegateTypeId = 2,
                    DelegateCompanyId = 2,
                    FullName = "",
                    IsActive = false,
                };
                delegateEntity = await _delegateRepository.AddAndSaveAsync(delegateEntity);
            }

            var providerDelegateEntity = await _providerDelegatesRepository.GetByDelegateIdandProviderIdAsync(delegateEntity.Id, providerId);
            if (providerDelegateEntity == null)
            {
                ProviderDelegateEntity providerDelegate = new()
                {
                    DelegateId = delegateEntity.Id,
                    ProviderId = providerId,
                };

                await _providerDelegatesRepository.AddAndSaveAsync(providerDelegate);
            }

            return provider;
        }

        /// <summary>
        /// Create provider delegate relation.
        /// </summary>
        /// <param name="providerId"></param>
        /// <param name="delegateId"></param>
        /// <returns></returns>
        /// <exception cref="DelegateNotFoundException"></exception>
        public async Task<ProviderDelegateEntity?> CreateProviderDelegateRelationAsync(int providerId, int delegateId)
        {
            // Verify ids are valid
            _ = await _providerService.GetProviderById(providerId);
            _ = await _delegateRepository.GetByIdAsync(delegateId)
                ?? throw new DelegateNotFoundException($"Delegate was not found by id [{delegateId}]");

            var providerDelegateEntity = await _providerDelegatesRepository.GetByDelegateIdandProviderIdAsync(delegateId, providerId);
            if (providerDelegateEntity == null)
            {
                providerDelegateEntity = new()
                {
                    DelegateId = delegateId,
                    ProviderId = providerId,
                };

                providerDelegateEntity = await _providerDelegatesRepository.AddAndSaveAsync(providerDelegateEntity);
            }

            return providerDelegateEntity;
        }

        /// <summary>
        /// Update delegate.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="fullName"></param>
        /// <returns></returns>
        /// <exception cref="DelegateNotFoundException"></exception>
        /// <exception cref="GenericDelegateException"></exception>
        public async Task<DelegateEntity> UpdateDelegateAsync(string email, string fullName)
        {
            var delegateEntity = await _context.Delegate.FirstOrDefaultAsync(d => d.Email == email)
                ?? throw new DelegateNotFoundException("UpdateDelegateError:  delegate not found bye email address.");

            if (!delegateEntity.FullName.IsNullOrEmpty())
            {
                throw new GenericDelegateException($"Delegate [{delegateEntity.Id}] is already registered.");
            }

            delegateEntity.FullName = fullName;
            delegateEntity.IsActive = true;

            return await _delegateRepository.UpdateAsync(delegateEntity);
        }
    }
}
