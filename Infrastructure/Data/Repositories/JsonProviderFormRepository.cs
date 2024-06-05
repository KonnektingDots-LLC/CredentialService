using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Infrastructure.Data.Repositories
{
    public class JsonProviderFormRepository : GenericAuditRepository<JsonProviderFormEntity, int>, IJsonProviderFormRepository
    {
        private readonly DbContextEntity _dbContextEntity;

        public JsonProviderFormRepository(DbContextEntity dbContextEntity, IHttpContextAccessor contextAccessor) : base(dbContextEntity, contextAccessor)
        {
            _dbContextEntity = dbContextEntity;
        }

        public async Task<JsonProviderFormEntity?> GetLatestByProviderId(int providerId)
        {
            return await _dbContextEntity.JsonProviderForm
                 .Where(x => x.ProviderId == providerId)
                 .OrderByDescending(x => x.Id).FirstOrDefaultAsync();
        }
    }
}
