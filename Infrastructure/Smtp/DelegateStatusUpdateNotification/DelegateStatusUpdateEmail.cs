using cred_system_back_end_app.Infrastructure.Smpt;
using cred_system_back_end_app.Infrastructure.Smtp.Template;

namespace cred_system_back_end_app.Infrastructure.Smtp.DelegateStatusUpdateNotification
{
    public class DelegateStatusUpdateEmail : EmailBase
    {
        public string ProviderName;

        public DelegateStatusUpdateEmail(SmtpClient smtpClient) 
            : base(smtpClient)
        {
            Subject = NotificationTemplate.DelegateStatusUpdate.Subject;
            Template = NotificationTemplate.DelegateStatusUpdate.Body;
        }

        public override string GetBody()
        {
            return Template
                .Replace(
                    NotificationTemplate.DelegateStatusUpdate.ProviderNameToken, 
                    ProviderName);
        }

    }
}
