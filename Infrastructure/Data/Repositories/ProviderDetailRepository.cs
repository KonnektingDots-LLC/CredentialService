using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Infrastructure.Data.Repositories
{
    public class ProviderDetailRepository : GenericAuditRepository<ProviderDetailEntity, int>, IProviderDetailRepository
    {
        private readonly DbContextEntity _dbContextEntity;

        public ProviderDetailRepository(DbContextEntity dbContextEntity, IHttpContextAccessor contextAccessor) : base(dbContextEntity, contextAccessor)
        {
            _dbContextEntity = dbContextEntity;
        }

        public async Task<ProviderDetailEntity?> GetByProviderId(int providerId)
        {
            return await _dbContextEntity.ProviderDetail
                .Where(p => p.ProviderId == providerId)
                .FirstOrDefaultAsync();
        }
    }
}
