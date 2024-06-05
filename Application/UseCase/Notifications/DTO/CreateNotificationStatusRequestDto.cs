namespace cred_system_back_end_app.Application.UseCase.Notifications.DTO
{
    public class CreateNotificationStatusRequestDto
    {
        public bool IsSuccess { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}