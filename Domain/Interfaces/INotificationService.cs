using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Domain.Interfaces
{
    public interface INotificationService
    {
        Task SendNotificationAsync(NotificationProfileCompletionDetailEntity notificationProfileCompletionDetailEntity, Func<Task<(string, string)>> sendEmailAction);
        Task SendNotificationAsync(NotificationEntity notificationEntity, Func<Task<(string, string)>> sendEmailAction);
        NotificationStatusEntity GetNotificationStatus(string receiverEmail, bool isSuccess = true);
        NotificationEntity GetNotificationEntity(int resourceId, string notificationTypeId, string resourceTypeId, string receiverEmail);
        NotificationEntity GetNotificationEntity(int resourceId, string notificationTypeId, string resourceTypeId, string receiverEmail, string sentBy);
        Task SaveNotification(Exception ex, NotificationEntity notification);
        Task SaveNotification(NotificationEntity notificationEntity);
    }
}
