namespace cred_system_back_end_app.Infrastructure.Smtp
{
    public class NotificationDto
    {
        public string? ResourceTypeId { get; set; }
        public string? ResourceId { get; set; }
        public string? NotificationTypeId { get; set; }
    }
}
