using cred_system_back_end_app.Application.DTO.Notifications;
using cred_system_back_end_app.Domain.Interfaces.Emails;
using static cred_system_back_end_app.Infrastructure.Smtp.Template.NotificationTemplate;

namespace cred_system_back_end_app.Infrastructure.Smtp.Emails
{
    public class DelegateInvitationNotificationEmail : EmailBase, IDelegateInvitationNotificationEmail<NotificationEmailDto>
    {
        private static readonly string subject = "OCS Credentialing System Delegate Invitation";

        public DelegateInvitationNotificationEmail(SmtpClient smtpClient) : base(smtpClient, subject, DelegateInvitationNotificationTemplate)
        {
        }

        public override string GetBody(NotificationEmailDto emailDto)
        {
            return _template.Replace("[Provider's Name]", emailDto.ProviderName).Replace("[link]", emailDto.Link);
        }
    }
}
