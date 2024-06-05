using cred_system_back_end_app.Application.DTO.Notifications;
using cred_system_back_end_app.Domain.Interfaces.Emails;
using static cred_system_back_end_app.Infrastructure.Smtp.Template.NotificationTemplate;

namespace cred_system_back_end_app.Infrastructure.Smtp.Emails
{
    public class ProviderReviewNotificationEmail : EmailBase, IProviderReviewNotificationEmail<NotificationEmailDto>
    {
        private static readonly string subject = "OCS Credentialing System Review and Submission by the Delegate";

        public ProviderReviewNotificationEmail(SmtpClient smtpClient) : base(smtpClient, subject, ProviderReviewNotificationTemplate)
        {
        }

        public override string GetBody(NotificationEmailDto emailDto)
        {
            return _template.Replace("[link]", emailDto.Link);
        }
    }
}
