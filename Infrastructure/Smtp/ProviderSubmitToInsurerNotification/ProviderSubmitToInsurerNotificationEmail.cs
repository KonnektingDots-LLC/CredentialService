using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Infrastructure.Smpt;
using cred_system_back_end_app.Infrastructure.Smtp.Template;

namespace cred_system_back_end_app.Infrastructure.Smtp.ProviderSubmitToInsurerNotification
{
    public class ProviderSubmitToInsurerNotificationEmail
    {
        private readonly SmtpClient _emailCase;

        private string subject = "OCS Credentialing System Provider's Profile Completion: Credentialing Process";

        private string template = NotificationTemplate.ProviderSubmitToInsurerNotificationTemplate;

        public ProviderSubmitToInsurerNotificationEmail(SmtpClient emailCase)
        {
            _emailCase = emailCase;
        }

        public async Task<CommResponseDto> SendEmailAsync(ProviderSubmitToInsurerRequestDto request)
        {
            ReplaceContent(request.ProviderName, request.Link);
            SmtpClientRequest email = new SmtpClientRequest();
            email.Body = template;
            email.Subject = subject;
            email.ToEmail = request.EmailTo;

            return await _emailCase.SendEmailAsync(email);

        }

        private void ReplaceContent(string name, string link)
        {
            template = template.Replace("[Provider's Name]", name);
            template = template.Replace("[link]", link);
        }
    }
}
