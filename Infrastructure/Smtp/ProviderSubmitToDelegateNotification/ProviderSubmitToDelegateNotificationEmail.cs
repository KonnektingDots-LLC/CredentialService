using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Infrastructure.Smpt;
using cred_system_back_end_app.Infrastructure.Smtp.Template;

namespace cred_system_back_end_app.Infrastructure.Smtp.ProviderSubmitToDelegateNotification
{
    public class ProviderSubmitToDelegateNotificationEmail
    {
        private readonly SmtpClient _emailCase;

        private string subject = "OCS Credentialing System Provider's Profile Completion: Credentialing Process";

        private string template = NotificationTemplate.ProviderSubmitToDelegateNotificationTemplate;

        public ProviderSubmitToDelegateNotificationEmail(SmtpClient emailCase)
        {
            _emailCase = emailCase;
        }

        public async Task<CommResponseDto> SendEmailAsync(ProviderSubmitToDelegateRequestDto request)
        {
            ReplaceContent(request.ProviderName);
            SmtpClientRequest email = new SmtpClientRequest();
            email.Body = template;
            email.Subject = subject;
            email.ToEmail = request.EmailTo;

            return await _emailCase.SendEmailAsync(email);
        }

        private void ReplaceContent(string name)
        {
            template = template.Replace("[Provider's Name]", name);
        }
    }
}
