using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Domain.Interfaces.Repositories
{
    public interface IDelegateRepository : IGenericRepository<DelegateEntity, int>
    {
        Task<DelegateEntity?> GetByEmailAsync(string delegateEmail);
        Task<DelegateEntity?> GetWithRelatedRefByEmailAsync(string delegateEmail);
    }
}
