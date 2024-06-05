using cred_system_back_end_app.Application.Common.Constant;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Infrastructure.Smpt;
using cred_system_back_end_app.Infrastructure.Smtp.ProviderCompletionNotification;
using cred_system_back_end_app.Infrastructure.Smtp.Template;

namespace cred_system_back_end_app.Infrastructure.Smtp.ProfileCompletionNotification
{
    public class ProfileCompletionNotificationEmail
    {
        private readonly SmtpClient _emailCase;

        private string subject = "OCS Credentialing System Account Created and Profile Completed";

        private string template = NotificationTemplate.ProfileCompletionNotificationTemplate;

        public ProfileCompletionNotificationEmail(SmtpClient emailCase)
        {
            _emailCase = emailCase;
        }

        public async Task<CommResponseDto> SendEmailAsync(string ToEmail)
        {
            SmtpClientRequest email = new SmtpClientRequest();
            email.Body = template;
            email.Subject = subject;
            email.ToEmail = ToEmail;
            return await _emailCase.SendEmailAsync(email);
        }
    }
}
