using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Domain.Interfaces.Repositories
{
    public interface ISpecialtyListRepository : IGenericRepository<SpecialtyListEntity, int>
    {
        Task<List<SpecialtyListEntity>> GetSpecialtyByOrganizationId(int organizationId);
    }
}
