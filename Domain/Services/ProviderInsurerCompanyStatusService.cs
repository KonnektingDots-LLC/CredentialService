
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Exceptions;
using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Domain.Interfaces.Repositories;

namespace cred_system_back_end_app.Domain.Services
{
    public class ProviderInsurerCompanyStatusService : IProviderInsurerCompanyStatusService
    {
        private readonly IProviderService _providerService;
        private readonly IInsurerCompanyRepository _insurerCompanyRepository;
        private readonly IProviderInsurerCompanyStatusRepository _providerInsurerCompanyStatusRepository;
        private readonly IGenericRepository<InsurerTypeEntity, int> _insurerTypeRepository;

        public ProviderInsurerCompanyStatusService(IProviderService providerService, IInsurerCompanyRepository insurerCompanyRepository,
            IProviderInsurerCompanyStatusRepository providerInsurerCompanyStatusRepository,
            IGenericRepository<InsurerTypeEntity, int> insurerTypeRepository)
        {
            _insurerCompanyRepository = insurerCompanyRepository;
            _providerService = providerService;
            _providerInsurerCompanyStatusRepository = providerInsurerCompanyStatusRepository;
            _insurerTypeRepository = insurerTypeRepository;
        }

        /// <summary>
        /// Update Provider Insurer Company Status
        /// </summary>
        /// <param name="originalproviderInsurerCompanyStatus"></param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException"></exception>
        public async Task<ProviderInsurerCompanyStatusEntity> UpdateProviderInsurerCompanyStatus(ProviderInsurerCompanyStatusEntity originalproviderInsurerCompanyStatus)
        {
            _ = await _insurerTypeRepository.GetByIdAsync(int.Parse(originalproviderInsurerCompanyStatus.InsurerStatusTypeId))
                ?? throw new EntityNotFoundException();

            var updateproviderInsurerCompanyStatus = await GetProviderInsurerCompanyStatusByIdAsync(originalproviderInsurerCompanyStatus.Id);

            updateproviderInsurerCompanyStatus.InsurerStatusTypeId = originalproviderInsurerCompanyStatus.InsurerStatusTypeId;
            updateproviderInsurerCompanyStatus.CurrentStatusDate = originalproviderInsurerCompanyStatus.CurrentStatusDate;
            updateproviderInsurerCompanyStatus.SubmitDate = originalproviderInsurerCompanyStatus.SubmitDate;
            updateproviderInsurerCompanyStatus.Comment = originalproviderInsurerCompanyStatus.Comment;
            updateproviderInsurerCompanyStatus.CommentDate = originalproviderInsurerCompanyStatus.CommentDate;
            updateproviderInsurerCompanyStatus.ModifiedBy = originalproviderInsurerCompanyStatus.ModifiedBy;
            updateproviderInsurerCompanyStatus.ModifiedDate = DateTime.Now;

            return await _providerInsurerCompanyStatusRepository.UpdateAsync(updateproviderInsurerCompanyStatus);
        }

        /// <summary>
        /// Get Provider Insurer Company Status By Provider Id And Insurer Company Id
        /// </summary>
        /// <param name="providerId"></param>
        /// <param name="insurerCompanyId"></param>
        /// <returns></returns>
        /// <exception cref="GenericInsurerException"></exception>
        /// <exception cref="EntityNotFoundException"></exception>
        public async Task<ProviderInsurerCompanyStatusEntity> GetProviderInsurerCompanyStatusByProviderIdAndInsurerCompanyId(int providerId, string insurerCompanyId)
        {
            // validate provider id
            _ = await _providerService.GetProviderById(providerId);

            // validate incurer company id
            _ = await _insurerCompanyRepository.GetByIdAsync(insurerCompanyId)
                ?? throw new GenericInsurerException($"Insurer company was not found by id [{insurerCompanyId}]");

            var providerInsurerCompanyStatusEntity = await _providerInsurerCompanyStatusRepository.GetByProviderIdAndInsurerCompanyIdAsync(providerId, insurerCompanyId)
                ?? throw new EntityNotFoundException($"Provider insurer company status was not found by provider id [{providerId}] and insurer company id [{insurerCompanyId}].");

            return providerInsurerCompanyStatusEntity;
        }

        /// <summary>
        /// Get Provider Insurer Company Status By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException"></exception>
        public async Task<ProviderInsurerCompanyStatusEntity?> GetProviderInsurerCompanyStatusByIdAsync(int id)
        {
            var providerInsurerCompanyStatusEntity = await _providerInsurerCompanyStatusRepository.GetByIdAsync(id)
                ?? throw new GenericInsurerException($"Provider insurer company status was not found by id [{id}]");
            return providerInsurerCompanyStatusEntity;
        }

        /// <summary>
        /// Get Provider Insurer Company Status By provider Id
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        /// <exception cref="GenericInsurerException"></exception>
        public async Task<IEnumerable<ProviderInsurerCompanyStatusEntity>> GetInsurerStatusesByProviderIdAsync(int providerId)
        {
            var providerInsurerCompanyStatusEntity = await _providerInsurerCompanyStatusRepository.SearchInsurerStatusesByProviderIdAsync(providerId)
                ?? throw new GenericInsurerException($"Provider insurer company status was not found by provider id [{providerId}]");
            return providerInsurerCompanyStatusEntity;
        }
    }
}
