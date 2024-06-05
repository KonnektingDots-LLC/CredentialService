using cred_system_back_end_app.Application.DTO.Notifications;
using cred_system_back_end_app.Domain.Interfaces.Emails;
using static cred_system_back_end_app.Infrastructure.Smtp.Template.NotificationTemplate.InsurerProviderStatusUpdate;

namespace cred_system_back_end_app.Infrastructure.Smtp.Emails
{
    public class InsurerToProviderStatusEmail : EmailBase, IInsurerToProviderStatusEmail<NotificationEmailDto>
    {
        public InsurerToProviderStatusEmail(SmtpClient smtpClient)
            : base(smtpClient, Subject, Body) { }

        public override string GetBody(NotificationEmailDto emailDto)
        {
            return _template
                .Replace(
                    ProviderNameToken,
                    emailDto.ProviderName)
                .Replace(Link,
                    emailDto.Link);
        }
    }
}
