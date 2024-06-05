namespace cred_system_back_end_app.Application.UseCase.Notifications.DTO
{
    public class CreateNotificationRequestDto
    {
        public string NotificationTypeId { get; set; }
        public int ResourceId { get; set; }
        public int NotificationStatusId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}