using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Domain.Interfaces.Repositories
{
    public interface IProviderDelegatesRepository : IGenericRepository<ProviderDelegateEntity, int>
    {
        Task<(List<ProviderDelegateEntity>, int)> SearchByProviderId(int providerId, int offset, int limit);
        Task<List<ProviderDelegateEntity>> SearchByProviderId(int providerId);
        Task<ProviderDelegateEntity?> GetByDelegateIdandProviderIdAsync(int delegateId, int providerId);
        Task UpdateIsActiveByDelegateIdAndProviderIdAsync(int delegateId, int providerId, bool isActive);
        Task<List<ProviderDelegateEntity>?> SearchByDelegateIdAsync(int delegateId);
    }
}
