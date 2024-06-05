using cred_system_back_end_app.Application.DTO.Notifications;
using cred_system_back_end_app.Domain.Interfaces.Emails;
using static cred_system_back_end_app.Infrastructure.Smtp.Template.NotificationTemplate.InsurerProviderStatusRTPUpdate;

namespace cred_system_back_end_app.Infrastructure.Smtp.Emails
{
    public class InsurerToProviderStatusRtpEmail : EmailBase, IInsurerToProviderStatusRtpEmail<NotificationEmailDto>
    {
        public InsurerToProviderStatusRtpEmail(SmtpClient smtpClient)
            : base(smtpClient, Subject, Body)
        {
        }
    }
}
