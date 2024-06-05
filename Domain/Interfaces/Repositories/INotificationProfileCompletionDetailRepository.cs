using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Domain.Interfaces.Repositories
{
    public interface INotificationProfileCompletionDetailRepository : IGenericRepository<NotificationProfileCompletionDetailEntity, int>
    {
        Task<NotificationProfileCompletionDetailEntity?> GetByEmailAsync(string email);
    }
}
