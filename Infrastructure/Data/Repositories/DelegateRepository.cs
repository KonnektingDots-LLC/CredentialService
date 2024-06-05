using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Infrastructure.Data.Repositories
{
    public class DelegateRepository : GenericAuditRepository<DelegateEntity, int>, IDelegateRepository
    {
        private readonly DbContextEntity _dbContextEntity;

        public DelegateRepository(DbContextEntity dbContextEntity, IHttpContextAccessor contextAccessor) : base(dbContextEntity, contextAccessor)
        {
            _dbContextEntity = dbContextEntity;
        }

        public async Task<DelegateEntity?> GetByEmailAsync(string delegateEmail)
        {
            return await _dbContextEntity.Delegate.Where(p => p.Email == delegateEmail).FirstOrDefaultAsync();
        }

        public async Task<DelegateEntity?> GetWithRelatedRefByEmailAsync(string delegateEmail)
        {
            return await _dbContextEntity.Delegate
                .Include(d => d.DelegateCompany)
                .Include(d => d.DelegateType)
                .Include(d => d.ProviderDelegate)
                .Where(p => p.Email == delegateEmail).FirstOrDefaultAsync();
        }
    }
}
