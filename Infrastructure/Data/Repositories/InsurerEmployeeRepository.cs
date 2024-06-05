using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Exceptions;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Infrastructure.Data.Repositories
{
    public class InsurerEmployeeRepository : GenericAuditRepository<InsurerEmployeeEntity, int>, IInsurerEmployeeRepository
    {

        private readonly DbContextEntity _dbContextEntity;

        public InsurerEmployeeRepository(DbContextEntity dbContextEntity,
            IHttpContextAccessor httpContextAccessor) : base(dbContextEntity, httpContextAccessor)
        {
            _dbContextEntity = dbContextEntity;
        }

        public async Task<(List<InsurerEmployeeEntity>, int)> SearchByInsurerCompanyIdAndSearchValue(string insurerCompanyId, string searchValue, int offset = 0, int limit = 50)
        {
            IQueryable<InsurerEmployeeEntity> query = _dbContextEntity.InsurerEmployee
                .Where(ie => ie.InsurerCompanyId == insurerCompanyId
                    && ((ie.Name + ie.MiddleName + ie.LastName + ie.SurName)
                 .Contains(searchValue) || ie.Email.Contains(searchValue)));
            return await Search(query, offset, limit);
        }

        public async Task<(List<InsurerEmployeeEntity>, int)> SearchByInsurerCompanyId(string insurerCompanyId, int offset = 0, int limit = 50)
        {
            IQueryable<InsurerEmployeeEntity> query = _dbContextEntity.InsurerEmployee
                .Where(ie => ie.InsurerCompanyId == insurerCompanyId);
            return await Search(query, offset, limit);
        }

        private static async Task<(List<InsurerEmployeeEntity>, int)> Search(IQueryable<InsurerEmployeeEntity> query, int offset, int limit)
        {
            int count = await query.CountAsync();
            if (count < offset)
            {
                throw new GenericInsurerException("Pagination offset exceeds record count.");
            }
            List<InsurerEmployeeEntity> result = await query
                           .Skip(offset)
                           .Take(limit)
                           .ToListAsync();

            return (result, count);
        }

        public async Task<InsurerEmployeeEntity?> GetByInsurerEmployeeEmailAsync(string email)
        {
            return await _dbContextEntity.InsurerEmployee
                .Where(i => i.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<InsurerEmployeeEntity>> SearchByInsurerEmployeeEmailAsync(string email, bool includeCompany = false)
        {
            if (includeCompany)
            {
                return await _dbContextEntity.InsurerEmployee
                  .Where(i => i.Email == email)
                  .Include(i => i.InsurerCompany)
                  .ToListAsync();
            }

            return await _dbContextEntity.InsurerEmployee
                .Where(i => i.Email == email)
                .ToListAsync();
        }
    }
}
