using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Domain.Interfaces.Repositories
{
    public interface ICredFormRepository : IGenericRepository<CredFormEntity, int>
    {
        new Task<CredFormEntity?> GetByIdAsync(int id);
        Task<CredFormEntity?> GetByEmailAsync(string email);
        Task SetStatusAndSave(int id, string newStatus);
        new Task<CredFormEntity> AddAndSaveAsync(CredFormEntity entity);
    }
}
