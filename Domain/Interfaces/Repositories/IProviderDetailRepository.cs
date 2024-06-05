using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Domain.Interfaces.Repositories
{
    public interface IProviderDetailRepository : IGenericRepository<ProviderDetailEntity, int>
    {
        Task<ProviderDetailEntity?> GetByProviderId(int providerId);
    }
}
