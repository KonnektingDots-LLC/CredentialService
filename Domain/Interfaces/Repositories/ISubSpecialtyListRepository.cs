using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Domain.Interfaces.Repositories
{
    public interface ISubSpecialtyListRepository : IGenericRepository<SubSpecialtyListEntity, int>
    {
        Task<List<SubSpecialtyListEntity>> GetSubSpecialtyByOrganizationId(int organizationId);
    }
}
