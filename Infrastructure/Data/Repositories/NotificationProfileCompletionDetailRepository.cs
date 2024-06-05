using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Infrastructure.Data.Repositories
{
    public class NotificationProfileCompletionDetailRepository : GenericAuditRepository<NotificationProfileCompletionDetailEntity, int>, INotificationProfileCompletionDetailRepository
    {
        private readonly DbContextEntity _dbContext;

        public NotificationProfileCompletionDetailRepository(DbContextEntity dbContextEntity, IHttpContextAccessor contextAccessor) : base(dbContextEntity, contextAccessor)
        {
            _dbContext = dbContextEntity;
        }

        public async Task<NotificationProfileCompletionDetailEntity?> GetByEmailAsync(string email)
        {
            return await _dbContext
                .NotificationProfileCompletionDetail
                .FirstOrDefaultAsync(n => n.Email == email);

        }
    }
}
