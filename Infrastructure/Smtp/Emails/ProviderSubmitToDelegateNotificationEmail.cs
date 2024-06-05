using cred_system_back_end_app.Application.DTO.Notifications;
using cred_system_back_end_app.Domain.Interfaces.Emails;
using static cred_system_back_end_app.Infrastructure.Smtp.Template.NotificationTemplate;

namespace cred_system_back_end_app.Infrastructure.Smtp.Emails
{
    public class ProviderSubmitToDelegateNotificationEmail : EmailBase, IProviderSubmitToDelegateNotificationEmail<NotificationEmailDto>
    {
        private static readonly string subject = "OCS Credentialing System Provider's Profile Completion: Credentialing Process";

        public ProviderSubmitToDelegateNotificationEmail(SmtpClient smtpClient) : base(smtpClient, subject, ProviderSubmitToDelegateNotificationTemplate)
        {
        }

        public override string GetBody(NotificationEmailDto emailDto)
        {
            return _template.Replace("[Provider's Name]", emailDto.ProviderName);
        }
    }
}
