namespace cred_system_back_end_app.Application.UseCase.Notifications.DTO
{
    public class CreateNotificationErrorRequestDto
    {
        public int NotificationStatusId { get; set; }
        public string ErrorDetail { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}