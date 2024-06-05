using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Infrastructure.Data.Repositories
{
    public class SubSpecialtyListRepository : GenericAuditRepository<SubSpecialtyListEntity, int>, ISubSpecialtyListRepository
    {
        private readonly DbContextEntity _dbContextEntity;

        public SubSpecialtyListRepository(DbContextEntity dbContextEntity, IHttpContextAccessor contextAccessor) : base(dbContextEntity, contextAccessor)
        {
            _dbContextEntity = dbContextEntity;
        }

        public async Task<List<SubSpecialtyListEntity>> GetSubSpecialtyByOrganizationId(int organizationId)
        {
            return await _dbContextEntity.SubSpecialtyList
                            .Where(sl => sl.OrganizationTypeId == organizationId)
                            .OrderBy(sl => sl.Name)
                            .ToListAsync();
        }
    }
}
