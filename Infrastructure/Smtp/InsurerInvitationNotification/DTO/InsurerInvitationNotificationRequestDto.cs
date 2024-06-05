namespace cred_system_back_end_app.Infrastructure.Smtp.InsurerInvitationNotification.DTO
{
    public class InsurerInvitationNotificationRequestDto
    {
        public string ToEmail { get; set; }

        public string InsurerName { get; set; }

        public string Link { get; set; }
    }
}
