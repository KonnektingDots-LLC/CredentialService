using cred_system_back_end_app.Application.DTO.Notifications;
using cred_system_back_end_app.Domain.Interfaces.Emails;
using static cred_system_back_end_app.Infrastructure.Smtp.Template.NotificationTemplate.ProviderDelegateStatusUpdate;

namespace cred_system_back_end_app.Infrastructure.Smtp.Emails
{
    public class ProviderDelegateStatusUpdateEmail : EmailBase, IProviderDelegateStatusUpdateEmail<NotificationEmailDto>
    {
        public ProviderDelegateStatusUpdateEmail(SmtpClient smptClient)
            : base(smptClient, Subject, Body)
        { }

        public override string GetBody(NotificationEmailDto emailDto)
        {
            return _template
                .Replace
                (
                    DelegateNameToken,
                    emailDto.DelegateName
                );
        }
    }
}
