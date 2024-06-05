using cred_system_back_end_app.Infrastructure.Smpt;
using cred_system_back_end_app.Infrastructure.Smtp.Template;

namespace cred_system_back_end_app.Infrastructure.Smtp.InsurerToProviderStatusRTPNotification
{
    public class InsurerToProviderStatusRTPEmail : EmailBase
    {
        

        public InsurerToProviderStatusRTPEmail(SmtpClient smtpClient)
            : base(smtpClient)
        {
            Subject = NotificationTemplate.InsurerProviderStatusRTPUpdate.Subject;
            Template = NotificationTemplate.InsurerProviderStatusRTPUpdate.Body;
        }

        public override string GetBody()
        {
            return Template;
        }
    }
}
