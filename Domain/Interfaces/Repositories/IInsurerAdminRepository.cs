using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Domain.Interfaces.Repositories
{
    public interface IInsurerAdminRepository : IGenericRepository<InsurerAdminEntity, int>
    {
        Task<InsurerAdminEntity?> GetByEmailAsync(string email);
    }
}
