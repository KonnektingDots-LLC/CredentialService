namespace cred_system_back_end_app.Application.DTO.Notifications
{
    public class NotificationEmailDto
    {
        public string? DelegateName { get; set; }
        public string? ProviderName { get; set; }
        public string? InsurerName { get; set; }
        public string? ToEmail { get; set; }
        public string? Link { get; set; }
        public int ProviderId { get; set; }

        public NotificationEmailDto() { }

        public NotificationEmailDto(string? ToEmail)
        {
            this.ToEmail = ToEmail;
        }
    }
}
