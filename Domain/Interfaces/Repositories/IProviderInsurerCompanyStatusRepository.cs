using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Domain.Interfaces.Repositories
{
    public interface IProviderInsurerCompanyStatusRepository : IGenericRepository<ProviderInsurerCompanyStatusEntity, int>
    {
        Task<(List<ProviderInsurerCompanyStatusEntity>, int)> SearchByInsurerCompanyIdAsync(string insurerCompanyId, int offset = 0, int limit = 50);
        Task<(List<ProviderInsurerCompanyStatusEntity>, int)> SearchByInsurerCompanyIdAndFullNameAsync(string insurerCompanyId, string fullname, int offset = 0, int limit = 50);
        Task<(List<ProviderInsurerCompanyStatusEntity>, int)> SearchByInsurerCompanyIdAndNpiAsync(string insurerCompanyId, string npiNumber, int offset = 0, int limit = 50);
        Task<List<ProviderInsurerCompanyStatusEntity>> SearchInsurerStatusesByProviderIdAsync(int providerId);
        Task<(List<ProviderInsurerCompanyStatusEntity>, int)> SearchInsurerStatusesByProviderIdAsync(int providerId, int offset, int limitPerPage);
        Task<ProviderInsurerCompanyStatusEntity?> GetByProviderIdAndInsurerCompanyIdAsync(int providerId, string insurerCompanyId);
    }
}
