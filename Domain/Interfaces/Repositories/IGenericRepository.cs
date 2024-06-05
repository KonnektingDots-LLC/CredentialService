using cred_system_back_end_app.Domain.Common;
using System.Linq.Expressions;

namespace cred_system_back_end_app.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T, in TId> where T : EntityAuditBase
    {
        Task<T?> GetByIdAsync(TId id);
        Task<T> AddAndSaveAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<List<T>> ListAsync();
        Task DeleteIfFoundAsync(Expression<Func<T, bool>> predicate);
    }
}
