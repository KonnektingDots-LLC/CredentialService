using cred_system_back_end_app.Infrastructure.Smtp.Template;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Infrastructure.Smpt;

namespace cred_system_back_end_app.Infrastructure.Smtp.ProviderSubmitNotification
{
    public class ProviderSubmitNotificationEmail
    {
        private readonly SmtpClient _emailCase;

        private string subject = "OCS Credentialing System Provider's Profile Completion: Credentialing Process";

        private string template = NotificationTemplate.ProviderSubmitNotificationTemplate;

        public ProviderSubmitNotificationEmail(SmtpClient emailCase)
        {
            _emailCase = emailCase;
        }

        public async Task<CommResponseDto> SendEmailAsync(ProviderSubmitRequestDto request)
        {

            SmtpClientRequest email = new SmtpClientRequest();
            email.Body = template;
            email.Subject = subject;
            email.ToEmail = request.EmailTo;

            return await _emailCase.SendEmailAsync(email);

        }
    }
}
