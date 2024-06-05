using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Domain.Interfaces
{
    public interface ICrendentialingFormService
    {
        Task<JsonProviderFormEntity> GetLatestSnapshotByProviderId(int providerId);
        Task<CredFormEntity?> GetById(int credFormId);
        Task<CredFormEntity?> CreateCredFormVersion(string email);
    }
}
