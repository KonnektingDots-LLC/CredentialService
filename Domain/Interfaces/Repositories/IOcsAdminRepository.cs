using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Domain.Interfaces.Repositories
{
    public interface IOcsAdminRepository : IGenericRepository<OCSAdminEntity, int>
    {
        Task<OCSAdminEntity?> GetByEmailAsync(string email);
    }
}
