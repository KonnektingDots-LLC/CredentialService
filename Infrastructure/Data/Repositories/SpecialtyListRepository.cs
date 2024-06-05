using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Infrastructure.Data.Repositories
{
    public class SpecialtyListRepository : GenericAuditRepository<SpecialtyListEntity, int>, ISpecialtyListRepository
    {
        private readonly DbContextEntity _dbContextEntity;

        public SpecialtyListRepository(DbContextEntity dbContextEntity, IHttpContextAccessor contextAccessor) : base(dbContextEntity, contextAccessor)
        {
            _dbContextEntity = dbContextEntity;
        }

        public async Task<List<SpecialtyListEntity>> GetSpecialtyByOrganizationId(int organizationId)
        {
            return await _dbContextEntity.SpecialtyList
                            .Where(
                                    sl => sl.OrganizationTypeId == organizationId
                                    && sl.IsActive && !sl.IsExpired)
                            .OrderBy(sl => sl.Name)
                            .ToListAsync();
        }
    }
}
