using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Infrastructure.Data.Repositories
{
    public class OcsAdminRepository : GenericAuditRepository<OCSAdminEntity, int>, IOcsAdminRepository
    {
        private readonly DbContextEntity _dbContextEntity;

        public OcsAdminRepository(DbContextEntity dbContextEntity, IHttpContextAccessor contextAccessor) : base(dbContextEntity, contextAccessor)
        {
            _dbContextEntity = dbContextEntity;
        }

        public async Task<OCSAdminEntity?> GetByEmailAsync(string email)
        {
            return await _dbContextEntity.OCSAdmin.Where(oa => oa.Email == email).FirstOrDefaultAsync();
        }
    }
}
