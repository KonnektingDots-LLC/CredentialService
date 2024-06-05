using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Domain.Interfaces
{
    public interface IDelegateService
    {
        Task<(ProviderEntity, DelegateEntity)> UpdateProviderDelegateStatusAsync(bool isActive, int delegateId, string providerEmail);
        Task<ProviderEntity?> CreateProviderDelegateRelationAsync(int providerId, string delegateEmail);
        Task<ProviderDelegateEntity?> CreateProviderDelegateRelationAsync(int providerId, int delegateId);
        Task<DelegateEntity> UpdateDelegateAsync(string email, string fullName);
        Task<IEnumerable<ProviderDelegateEntity>> GetProviderDelegatesByProviderId(int providerId);
    }
}
