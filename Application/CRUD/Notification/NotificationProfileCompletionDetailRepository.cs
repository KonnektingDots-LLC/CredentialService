using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Application.CRUD.Notification
{
    public class NotificationProfileCompletionDetailRepository
    {
        private readonly DbContextEntity _dbContext;

        public NotificationProfileCompletionDetailRepository(DbContextEntity dbContextEntity) 
        { 
            _dbContext = dbContextEntity;
        }

        public async Task<NotificationProfileCompletionDetailEntity> GetByEmailAsync(string email) 
        {
            return await _dbContext
                .NotificationProfileCompletionDetail
                .FirstOrDefaultAsync(n => n.Email == email);
                
        }

        public async Task Set(NotificationProfileCompletionDetailEntity profileCompletionDetailEntity)
        {
            _dbContext.Add(profileCompletionDetailEntity);

            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            await _dbContext.SaveChangesAsync();

            await transaction.CommitAsync();
        }
    }
}
