using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Infrastructure.Data.Repositories
{
    public class ProviderRepository : GenericAuditRepository<ProviderEntity, int>, IProviderRepository
    {
        private readonly DbContextEntity _dbContextEntity;

        public ProviderRepository(DbContextEntity dbContextEntity, IHttpContextAccessor contextAccessor) : base(dbContextEntity, contextAccessor)
        {
            _dbContextEntity = dbContextEntity;
        }

        public async Task<ProviderEntity?> GetByEmailAsync(string email)
        {
            return await _dbContextEntity.Provider
                .Where(p => p.Email == email)
                .Include(p => p.CredForm)
                .ThenInclude(c => c.CredFormStatusType)
                .FirstOrDefaultAsync();
        }

        public async Task<(List<ProviderEntity>, int)> SearchAllAsync(int offset = 0, int limit = 50)
        {
            return await Search(_dbContextEntity.Provider, offset, limit);
        }

        public async Task<(List<ProviderEntity>, int)> SearchByFullNameAsync(string fullname, int offset = 0, int limit = 50)
        {
            IQueryable<ProviderEntity> query = _dbContextEntity.Provider.Where(p => (p.FirstName + p.MiddleName + p.LastName + p.SurName).Contains(fullname));
            return await Search(query, offset, limit);
        }

        public async Task<(List<ProviderEntity>, int)> SearchByNpiAsync(string npiNumber, int offset = 0, int limit = 50)
        {
            IQueryable<ProviderEntity> query = _dbContextEntity.Provider.Where(p => p.BillingNPI == npiNumber || p.RenderingNPI == npiNumber);
            return await Search(query, offset, limit);
        }

        private static async Task<(List<ProviderEntity>, int)> Search(IQueryable<ProviderEntity> query, int offset, int limit)
        {
            int count = await query.CountAsync();
            List<ProviderEntity> result = await query
                .Include(r => r.CredForm)
                           .ThenInclude(c => c.CredFormStatusType)
                           .OrderBy(o => o.CredForm.CredFormStatusType.PrioritySorting)
                           .Skip(offset)
                           .Take(limit)
                           .ToListAsync();

            return (result, count);
        }
    }
}
