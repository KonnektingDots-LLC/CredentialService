using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Domain.Interfaces.Repositories
{
    public interface IProviderCorporationRepository : IGenericRepository<ProviderCorporationEntity, int>
    {
        Task<List<ProviderCorporationEntity>> GetProviderCorporationsByProviderId(int providerId);
    }
}
