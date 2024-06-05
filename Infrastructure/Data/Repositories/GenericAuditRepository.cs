using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Domain.Common;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace cred_system_back_end_app.Infrastructure.Data.Repositories
{
    public class GenericAuditRepository<T, TId> : IGenericRepository<T, TId> where T : EntityAuditBase
    {
        private readonly DbContextEntity _dbContextEntity;
        private readonly IHttpContextAccessor _contextAccessor;

        public GenericAuditRepository(DbContextEntity dbContextEntity, IHttpContextAccessor contextAccessor)
        {
            _dbContextEntity = dbContextEntity;
            _contextAccessor = contextAccessor;

        }

        public virtual async Task<T> AddAndSaveAsync(T entity)
        {
            entity.CreationDate = DateTime.Now;
            entity.CreatedBy ??= GetLoggedUserEmail();

            await _dbContextEntity.Set<T>().AddAsync(entity);
            await _dbContextEntity.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = GetLoggedUserEmail();
            await _dbContextEntity.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<T?> GetByIdAsync(TId id)
        {
            return await _dbContextEntity.Set<T>().FindAsync(id);
        }

        public virtual async Task<List<T>> ListAsync()
        {
            return await _dbContextEntity.Set<T>().ToListAsync();
        }

        public virtual async Task DeleteIfFoundAsync(Expression<Func<T, bool>> predicate)
        {
            var entity = await _dbContextEntity.Set<T>().Where(predicate).FirstOrDefaultAsync();
            if (entity != null)
            {
                _dbContextEntity.Remove(entity);
                await _dbContextEntity.SaveChangesAsync();
            }
        }

        protected string? GetLoggedUserEmail()
        {
            var httpContext = _contextAccessor.HttpContext;
            var user = httpContext?.User;

            return user?.FindFirst(CredTokenKey.EMAIL)?.Value ?? "anonymous";
        }
    }
}
