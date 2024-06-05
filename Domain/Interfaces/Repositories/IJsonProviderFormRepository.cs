using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Domain.Interfaces.Repositories
{
    public interface IJsonProviderFormRepository : IGenericRepository<JsonProviderFormEntity, int>
    {
        public Task<JsonProviderFormEntity?> GetLatestByProviderId(int providerId);
    }
}
