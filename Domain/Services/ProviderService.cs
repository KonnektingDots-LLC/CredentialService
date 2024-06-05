using AutoMapper;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Exceptions;
using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.IdentityModel.Tokens;

namespace cred_system_back_end_app.Domain.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IGenericRepository<ProviderTypeEntity, int> _providerTypeEntityRepository;
        private readonly IGenericRepository<PlanAcceptListEntity, int> _planAcceptListEntityRepository;

        private readonly ICrendentialingFormService _crendentialingFormService;

        private readonly DbContextEntity _context;
        private readonly IProviderRepository _providerRepository;
        private readonly IProviderInsurerCompanyStatusRepository _providerInsurerCompanyStatusRepository;

        public ProviderService(ICrendentialingFormService crendentialingFormService, IProviderRepository providerRepository, IGenericRepository<ProviderTypeEntity, int> providerTypeEntityRepository,
            IGenericRepository<PlanAcceptListEntity, int> planAcceptListEntityRepository,
            IProviderInsurerCompanyStatusRepository providerInsurerCompanyStatusRepository, DbContextEntity context, IMapper mapper)
        {
            _crendentialingFormService = crendentialingFormService;
            _context = context;
            _providerRepository = providerRepository;
            _providerInsurerCompanyStatusRepository = providerInsurerCompanyStatusRepository;
            _providerTypeEntityRepository = providerTypeEntityRepository;
            _planAcceptListEntityRepository = planAcceptListEntityRepository;
        }

        /// <summary>
        /// Search all providers by NPI or full name restricted by offset and limit
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<(List<ProviderEntity>, int)> SearchProviders(int offset, int limit, string? search)
        {
            if (!search.IsNullOrEmpty())
            {
                var searchFullname = search.Replace(" ", "");
                static bool isAllDigits(string search) => search.All(char.IsDigit);
                if (!isAllDigits(search))
                {
                    return await _providerRepository.SearchByFullNameAsync(searchFullname, offset, limit);
                }
                else
                {
                    return await _providerRepository.SearchByNpiAsync(search, offset, limit);
                }
            }

            return await _providerRepository.SearchAllAsync(offset, limit);
        }

        /// <summary>
        /// Return a provider entity by provider ID
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        /// <exception cref="ProviderNotFoundException"></exception>
        public async Task<ProviderEntity?> GetProviderById(int providerId)
        {
            var provider = await _providerRepository.GetByIdAsync(providerId)
                ?? throw new ProviderNotFoundException($"Provider was not found by id: {providerId}.");

            return provider;
        }

        public ProviderEntity GetProviderEntityById(int id)
        {
            try
            {
                return _context.Provider.Find(id);

            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Return a list of provider type entities
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ProviderTypeNotFoundException"></exception>
        public async Task<List<ProviderTypeEntity>> GetAllProviderTypes()
        {
            var providerType = await _providerTypeEntityRepository.ListAsync();
            if (providerType.IsNullOrEmpty())
            {
                throw new ProviderTypeNotFoundException();
            }
            return providerType;
        }

        /// <summary>
        /// Return a list of plan accept entities
        /// </summary>
        /// <returns></returns>
        /// <exception cref="PlanAcceptNotFoundException"></exception>
        public async Task<List<PlanAcceptListEntity>> GetAllAcceptPlanList()
        {
            var planAccept = await _planAcceptListEntityRepository.ListAsync();
            if (planAccept.IsNullOrEmpty())
            {
                throw new PlanAcceptNotFoundException();
            }
            return planAccept;
        }

        /// <summary>
        /// Get a provider by its email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="ProviderNotFoundException"></exception>
        public async Task<ProviderEntity?> GetProviderByEmail(string email)
        {
            var provider = await _providerRepository.GetByEmailAsync(email);
            return provider ?? throw new ProviderNotFoundException("Searching provider by email failed, Provider was not found.");
        }

        /// <summary>
        /// Search all providers by insurer company id and (NPI or full name) restricted by offset and limit
        /// </summary>
        /// <param name="insurerCompanyId"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        /// <exception cref="GenericDelegateException"></exception>
        public async Task<(IEnumerable<ProviderInsurerCompanyStatusEntity>, int)>
            SearchProviderInsurerCompanyStatusesByInsurerCompany(string insurerCompanyId, int offset, int limit, string? search)
        {
            try
            {
                if (search.IsNullOrEmpty())
                {
                    return await _providerInsurerCompanyStatusRepository.SearchByInsurerCompanyIdAsync(insurerCompanyId, offset, limit);
                }
                else
                {
                    bool IsAllDigits(string search) => search.All(char.IsDigit);
                    if (!IsAllDigits(search))
                    {
                        var searchFullname = search.Replace(" ", "");
                        return await _providerInsurerCompanyStatusRepository.SearchByInsurerCompanyIdAndFullNameAsync(insurerCompanyId, searchFullname, offset, limit);
                    }
                    else
                    {
                        return await _providerInsurerCompanyStatusRepository.SearchByInsurerCompanyIdAndNpiAsync(insurerCompanyId, search, offset, limit);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new GenericProviderException($"An exception occurred while searching for providers by insurer company id [{insurerCompanyId}].", ex);
            }
        }

        public async Task<(ProviderEntity, CredFormEntity)> CreateProviderWithNewCredFormVersion(ProviderEntity newProvider)
        {
            var savedCredFormEntity = await _crendentialingFormService.CreateCredFormVersion(newProvider.Email);
            newProvider.CredForm = savedCredFormEntity;
            var savedProviderEntity = await _providerRepository.AddAndSaveAsync(newProvider);
            return (savedProviderEntity, savedCredFormEntity);
        }
    }
}
