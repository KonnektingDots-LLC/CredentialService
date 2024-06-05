using cred_system_back_end_app.Application.DTO.Notifications;
using cred_system_back_end_app.Domain.Interfaces.Emails;
using static cred_system_back_end_app.Infrastructure.Smtp.Template.NotificationTemplate.DelegateStatusUpdate;

namespace cred_system_back_end_app.Infrastructure.Smtp.Emails
{
    public class DelegateStatusUpdateEmail : EmailBase, IDelegateStatusUpdateEmail<NotificationEmailDto>
    {
        public DelegateStatusUpdateEmail(SmtpClient smtpClient)
            : base(smtpClient, Subject, Body)
        {
        }

        public override string GetBody(NotificationEmailDto emailDto)
        {
            return _template
                .Replace(
                    ProviderNameToken,
                    emailDto.ProviderName);
        }
    }
}
