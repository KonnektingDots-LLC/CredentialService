using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;

namespace cred_system_back_end_app.Application.CRUD.AssociativeEntities
{
    public class ProviderMedicalGroupRepository
    {
        private readonly DbContextEntity _dbContext;

        public ProviderMedicalGroupRepository(DbContextEntity contextEntity) 
        {
            _dbContext = contextEntity;
        }

        public ProviderMedicalGroupEntity GetProviderMedicalGroupByProviderId(int providerId) 
        {
            return _dbContext.ProviderMedicalGroup
                .Where(pmg => pmg.ProviderId == providerId)
                .ToList()
                .FirstOrDefault();
        }
    }
}
