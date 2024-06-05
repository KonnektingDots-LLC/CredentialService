using cred_system_back_end_app.Infrastructure.Smpt;
using cred_system_back_end_app.Infrastructure.Smtp.Template;


namespace cred_system_back_end_app.Infrastructure.Smtp.InsurerToProviderStatusNotification
{

    public class InsurerToProviderStatusEmail : EmailBase
    {
        public string ProviderName;
        public string WebsiteLink;

        public InsurerToProviderStatusEmail(SmtpClient smtpClient)
            : base(smtpClient)
        {
            Subject = NotificationTemplate.InsurerProviderStatusUpdate.Subject;
            Template = NotificationTemplate.InsurerProviderStatusUpdate.Body;
        }

        public override string GetBody()
        {
            return Template
                .Replace(
                    NotificationTemplate.InsurerProviderStatusUpdate.ProviderNameToken,
                    ProviderName)
                .Replace(NotificationTemplate.InsurerProviderStatusUpdate.Link,
                    WebsiteLink);
        }
    }
}
