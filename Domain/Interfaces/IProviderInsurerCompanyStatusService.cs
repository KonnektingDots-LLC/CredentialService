using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Domain.Interfaces
{
    public interface IProviderInsurerCompanyStatusService
    {
        Task<ProviderInsurerCompanyStatusEntity> GetProviderInsurerCompanyStatusByProviderIdAndInsurerCompanyId(int providerId, string insurerCompanyId);
        Task<ProviderInsurerCompanyStatusEntity?> GetProviderInsurerCompanyStatusByIdAsync(int id);
        Task<IEnumerable<ProviderInsurerCompanyStatusEntity>> GetInsurerStatusesByProviderIdAsync(int providerId);
        Task<ProviderInsurerCompanyStatusEntity> UpdateProviderInsurerCompanyStatus(ProviderInsurerCompanyStatusEntity originalproviderInsurerCompanyStatus);
    }
}
