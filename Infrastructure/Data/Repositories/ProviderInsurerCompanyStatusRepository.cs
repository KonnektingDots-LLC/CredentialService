using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Exceptions;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Infrastructure.Data.Repositories
{
    public class ProviderInsurerCompanyStatusRepository : GenericAuditRepository<ProviderInsurerCompanyStatusEntity, int>, IProviderInsurerCompanyStatusRepository
    {
        private readonly DbContextEntity _dbContextEntity;

        public ProviderInsurerCompanyStatusRepository(DbContextEntity dbContextEntity, IHttpContextAccessor contextAccessor) : base(dbContextEntity, contextAccessor)
        {
            _dbContextEntity = dbContextEntity;
        }

        public async Task<(List<ProviderInsurerCompanyStatusEntity>, int)> SearchByInsurerCompanyIdAsync(string insurerCompanyId, int offset = 0, int limit = 50)
        {
            return await Search(_dbContextEntity.ProviderInsurerCompanyStatus
                        .Where(pics => pics.InsurerCompanyId == insurerCompanyId), offset, limit);
        }

        public async Task<(List<ProviderInsurerCompanyStatusEntity>, int)> SearchByInsurerCompanyIdAndFullNameAsync(string insurerCompanyId, string fullname, int offset = 0, int limit = 50)
        {
            IQueryable<ProviderInsurerCompanyStatusEntity> query = _dbContextEntity.ProviderInsurerCompanyStatus
                        .Where(pics => pics.InsurerCompanyId == insurerCompanyId
                            && (pics.Provider.FirstName + pics.Provider.MiddleName + pics.Provider.LastName + pics.Provider.SurName)
                            .Contains(fullname));
            return await Search(query, offset, limit);
        }

        public async Task<(List<ProviderInsurerCompanyStatusEntity>, int)> SearchByInsurerCompanyIdAndNpiAsync(string insurerCompanyId, string npiNumber, int offset = 0, int limit = 50)
        {
            IQueryable<ProviderInsurerCompanyStatusEntity> query = _dbContextEntity.ProviderInsurerCompanyStatus
                        .Where(pics => pics.InsurerCompanyId == insurerCompanyId
                            && (pics.Provider.BillingNPI == npiNumber || pics.Provider.RenderingNPI == npiNumber));
            return await Search(query, offset, limit);
        }

        private static async Task<(List<ProviderInsurerCompanyStatusEntity>, int)> Search(IQueryable<ProviderInsurerCompanyStatusEntity> query, int offset, int limit)
        {
            int count = await query.CountAsync();
            List<ProviderInsurerCompanyStatusEntity> result = await query
                .Include(pics => pics.Provider)
                        .Include(pics => pics.InsurerStatusType)
                        .OrderBy(pics => pics.InsurerStatusType.PrioritySorting)
                        .Skip(offset)
                        .Take(limit)
                        .ToListAsync();

            return (result, count);
        }

        public async Task<List<ProviderInsurerCompanyStatusEntity>> SearchInsurerStatusesByProviderIdAsync(int providerId)
        {
            return await _dbContextEntity.ProviderInsurerCompanyStatus.Where(r => r.ProviderId == providerId)
                                                                        .Include(r => r.InsurerCompany)
                                                                        .ToListAsync();
        }

        public async Task<(List<ProviderInsurerCompanyStatusEntity>, int)> SearchInsurerStatusesByProviderIdAsync(int providerId, int offset, int limitPerPage)
        {
            var providerInsurerCompanyStatuses = _dbContextEntity.ProviderInsurerCompanyStatus
                                                            .Where(r => r.ProviderId == providerId)
                                                            .Include(r => r.InsurerCompany)
                                                            .Include(r => r.Provider)
                                                            .Include(r => r.InsurerStatusType);

            var statusesCount = providerInsurerCompanyStatuses.Count();

            var paginatedStatuses = await providerInsurerCompanyStatuses
                .Skip(offset)
                .Take(limitPerPage)
                .OrderBy(p => p.InsurerStatusType.PrioritySorting)
                .ToListAsync();

            if (paginatedStatuses.Count == 0)
            {
                throw new EntityNotFoundException();
            }

            return (paginatedStatuses, statusesCount);
        }

        public async Task<ProviderInsurerCompanyStatusEntity?> GetByProviderIdAndInsurerCompanyIdAsync(int providerId, string insurerCompanyId)
        {
            return await _dbContextEntity.ProviderInsurerCompanyStatus.Where(r => r.ProviderId == providerId
                               && r.InsurerCompanyId == insurerCompanyId.ToString()).FirstOrDefaultAsync();
        }
    }
}
