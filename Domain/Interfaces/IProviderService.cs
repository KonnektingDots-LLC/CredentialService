using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Domain.Interfaces
{
    public interface IProviderService
    {
        Task<(List<ProviderEntity>, int)> SearchProviders(int offset, int limit, string? search);
        Task<ProviderEntity?> GetProviderByEmail(string email);
        Task<ProviderEntity?> GetProviderById(int providerId);
        Task<(IEnumerable<ProviderInsurerCompanyStatusEntity>, int)>
            SearchProviderInsurerCompanyStatusesByInsurerCompany(string insurerCompanyId, int offset, int limit, string? search);
        Task<List<ProviderTypeEntity>> GetAllProviderTypes();
        Task<List<PlanAcceptListEntity>> GetAllAcceptPlanList();
        Task<(ProviderEntity, CredFormEntity)> CreateProviderWithNewCredFormVersion(ProviderEntity newProvider);
    }
}
