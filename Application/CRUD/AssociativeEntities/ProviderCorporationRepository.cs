using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;

namespace cred_system_back_end_app.Application.CRUD.AssociativeEntities
{
    public class ProviderCorporationRepository
    {
        private readonly DbContextEntity _dbContext;

        public ProviderCorporationRepository(DbContextEntity dbContextEntity) 
        {
            _dbContext = dbContextEntity;
        }

        public ProviderCorporationEntity GetProviderCorporationByProviderId(int providerId) 
        {
            return _dbContext.ProviderCorporation
                    .Where(providerCorporation => providerCorporation.ProviderId == providerId)
                    .ToList()
                    .FirstOrDefault();
        }
    }
}
