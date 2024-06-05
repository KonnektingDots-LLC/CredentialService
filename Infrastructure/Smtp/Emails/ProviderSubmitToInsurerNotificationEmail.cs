using cred_system_back_end_app.Application.DTO.Notifications;
using cred_system_back_end_app.Domain.Interfaces.Emails;
using static cred_system_back_end_app.Infrastructure.Smtp.Template.NotificationTemplate;

namespace cred_system_back_end_app.Infrastructure.Smtp.Emails
{
    public class ProviderSubmitToInsurerNotificationEmail : EmailBase, IProviderSubmitToInsurerNotificationEmail<NotificationEmailDto>
    {
        private static readonly string subject = "OCS Credentialing System Provider's Profile Completion: Credentialing Process";

        public ProviderSubmitToInsurerNotificationEmail(SmtpClient smtpClient) : base(smtpClient, subject, ProviderSubmitToInsurerNotificationTemplate)
        {
        }

        public override string GetBody(NotificationEmailDto emailDto)
        {
            return _template
                .Replace
                (
                    "[Provider's Name]",
                    emailDto.ProviderName
                )
                .Replace
                (
                    "[link]",
                    emailDto.Link
                );
        }
    }
}
