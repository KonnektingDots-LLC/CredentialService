using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Domain.Interfaces.Repositories
{
    public interface IAttestationRepository : IGenericRepository<AttestationEntity, int>
    {
        Task<AttestationEntity> GetAttestationByProviderIdAsync(int providerId);
    }
}
