using cred_system_back_end_app.Infrastructure.DB.Entity;

namespace cred_system_back_end_app.Application.UseCase.Notifications.DTO
{
    public class NotificationRequestDto
    {
        public NotificationEntity CreateNotification { get; set; }
        public NotificationStatusEntity CreateNotificationStatus { get; set; }
        public NotificationErrorEntity? CreateNotificationError { get; set; }
    }
}