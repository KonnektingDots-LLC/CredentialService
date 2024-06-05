using cred_system_back_end_app.Application.DTO.Notifications;
using cred_system_back_end_app.Domain.Interfaces.Emails;
using static cred_system_back_end_app.Infrastructure.Smtp.Template.NotificationTemplate;

namespace cred_system_back_end_app.Infrastructure.Smtp.Emails
{
    public class ProfileCompletionNotificationEmail : EmailBase, IProfileCompletionNotificationEmail<NotificationEmailDto>
    {
        private static readonly string subject = "OCS Credentialing System Account Created and Profile Completed";

        public ProfileCompletionNotificationEmail(SmtpClient smtpClient) : base(smtpClient, subject, ProfileCompletionNotificationTemplate)
        {
        }
    }
}
