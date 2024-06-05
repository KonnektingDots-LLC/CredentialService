using cred_system_back_end_app.Application.DTO.Notifications;
using cred_system_back_end_app.Domain.Interfaces.Emails;
using static cred_system_back_end_app.Infrastructure.Smtp.Template.NotificationTemplate;

namespace cred_system_back_end_app.Infrastructure.Smtp.Emails
{
    public class InsurerInvitationNotificationEmail : EmailBase, IInsurerInvitationNotificationEmail<NotificationEmailDto>
    {
        private static readonly string subject = "OCS Credentialing System Insurer Invitation";

        public InsurerInvitationNotificationEmail(SmtpClient smtpClient) : base(smtpClient, subject, InsurerEmployeeNotificationTemplate)
        {
        }

        public override string GetBody(NotificationEmailDto emailDto)
        {
            return _template.Replace("[Insurer's Name]", emailDto.InsurerName)
                .Replace("[link]", emailDto.Link);
        }
    }
}
