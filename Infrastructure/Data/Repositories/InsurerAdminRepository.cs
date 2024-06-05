using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Infrastructure.Data.Repositories
{
    public class InsurerAdminRepository : GenericAuditRepository<InsurerAdminEntity, int>, IInsurerAdminRepository
    {
        private readonly DbContextEntity _dbContext;

        public InsurerAdminRepository(DbContextEntity dbContext, IHttpContextAccessor contextAccessor) : base(dbContext, contextAccessor)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Return an insurer admin by email.
        /// </summary>
        /// <param name="email">the insurer admin email.</param>
        /// <returns></returns>
        public async Task<InsurerAdminEntity?> GetByEmailAsync(string email)
        {
            return await _dbContext.InsurerAdmin
                        .Include(x => x.InsurerCompany)
                        .Where(ia => ia.Email == email)
                        .FirstOrDefaultAsync();
        }
    }
}
