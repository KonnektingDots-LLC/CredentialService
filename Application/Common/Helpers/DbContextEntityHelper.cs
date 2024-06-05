using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Application.Common.Helpers
{
    public static class DbContextEntityHelper
    {
        public static async Task SaveAsTransactionAsync<T>(this DbContextEntity contextEntity, T entity) 
        { 
            using var transaction = await contextEntity.Database.BeginTransactionAsync();

            await contextEntity.AddAsync(entity);

            await contextEntity.SaveChangesAsync();

            await transaction.CommitAsync();
        }

        public static async Task UpdateAsync<T>(this DbContextEntity contextEntity, T entity)
        {
            contextEntity.Entry(entity).State = EntityState.Modified;

            await contextEntity.SaveChangesAsync();
        }
    }
}
