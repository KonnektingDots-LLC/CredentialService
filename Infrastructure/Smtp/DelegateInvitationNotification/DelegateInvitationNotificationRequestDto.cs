
namespace cred_system_back_end_app.Infrastructure.Smtp.DelegateInvitationNotification
{
    public class DelegateInvitationNotificationRequestDto
    {
        public string ToEmail { get; set; }

        public string ProviderName { get; set; }

        public string Link { get; set; }

        public int ProviderId { get; set; }

    }
}
