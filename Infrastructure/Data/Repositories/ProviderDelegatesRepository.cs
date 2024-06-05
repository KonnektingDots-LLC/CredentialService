using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Infrastructure.Data.Repositories
{
    public class ProviderDelegatesRepository : GenericAuditRepository<ProviderDelegateEntity, int>, IProviderDelegatesRepository
    {
        private readonly DbContextEntity _dbContextEntity;
        public ProviderDelegatesRepository(DbContextEntity dbContextEntity, IHttpContextAccessor contextAccessor) : base(dbContextEntity, contextAccessor)
        {
            _dbContextEntity = dbContextEntity;
        }

        public async Task<ProviderDelegateEntity?> GetByDelegateIdandProviderIdAsync(int delegateId, int providerId)
        {
            return await _dbContextEntity.ProviderDelegate
                .Where(pd => pd.ProviderId == providerId && pd.DelegateId == delegateId)
                .Include(d => d.Delegate)
                .FirstOrDefaultAsync();
        }

        public async Task<List<ProviderDelegateEntity>?> SearchByDelegateIdAsync(int delegateId)
        {
            return await _dbContextEntity.ProviderDelegate
                .Include(pd => pd.Provider)
                .Where(e => e.DelegateId == delegateId && e.IsActive)
                .ToListAsync();
        }

        public async Task<(List<ProviderDelegateEntity>, int)> SearchByProviderId(int providerId, int offset = 0, int limit = 50)
        {
            var delegateCount = await _dbContextEntity.ProviderDelegate
                .CountAsync(pd => pd.ProviderId == providerId);

            var providerDelegates = await _dbContextEntity.ProviderDelegate
                .Where(pd => pd.ProviderId == providerId)
                .Include(pd => pd.Delegate)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();

            return (providerDelegates, delegateCount);
        }

        public async Task<List<ProviderDelegateEntity>> SearchByProviderId(int providerId)
        {
            return await _dbContextEntity.ProviderDelegate
                .Where(p => p.ProviderId == providerId)
                .Include(p => p.Delegate)
                .ToListAsync();
        }

        public async Task UpdateIsActiveByDelegateIdAndProviderIdAsync(int delegateId, int providerId, bool isActive)
        {
            await _dbContextEntity.ProviderDelegate.Where(pd => pd.ProviderId == providerId && pd.DelegateId == delegateId)
                .ExecuteUpdateAsync(pd => pd
                .SetProperty(p => p.IsActive, isActive)
                .SetProperty(p => p.ModifiedDate, DateTime.Now)
                .SetProperty(p => p.ModifiedBy, GetLoggedUserEmail()));
        }
    }
}
