using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Domain.Interfaces.Repositories
{
    public interface IProviderInsurerCompanyStatusHistoryRepository : IGenericRepository<ProviderInsurerCompanyStatusHistoryEntity, int>
    {
        Task<List<ProviderInsurerCompanyStatusHistoryEntity>> GetProviderInsurerCompanyStatusHistoryByProviderInsurerCompanyStatusId(int providerInsurerCompanyStatusId);
    }
}
