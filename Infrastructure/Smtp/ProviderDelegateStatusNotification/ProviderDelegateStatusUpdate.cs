using cred_system_back_end_app.Infrastructure.Smpt;
using cred_system_back_end_app.Infrastructure.Smtp.Template;

namespace cred_system_back_end_app.Infrastructure.Smtp.ProviderDelegateUpdateNotification
{
    public class ProviderDelegateStatusUpdate : EmailBase
    {
        public string DelegateName;

        public ProviderDelegateStatusUpdate(SmtpClient smptClient) 
            : base(smptClient)
        {
            Subject = NotificationTemplate.ProviderDelegateStatusUpdate.Subject;
            Template = NotificationTemplate.ProviderDelegateStatusUpdate.Body;
        }

        public override string GetBody()
        {
            return Template
                .Replace
                (
                    NotificationTemplate.ProviderDelegateStatusUpdate.DelegateNameToken, 
                    DelegateName
                );
        }
    }
}
