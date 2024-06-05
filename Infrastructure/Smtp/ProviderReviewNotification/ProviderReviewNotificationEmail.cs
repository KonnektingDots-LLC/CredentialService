using cred_system_back_end_app.Application.Common.Constant;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Infrastructure.Smpt;
using cred_system_back_end_app.Infrastructure.Smtp.Template;

namespace cred_system_back_end_app.Infrastructure.Smtp.ProviderReviewNotification
{
    public class ProviderReviewNotificationEmail
    {
        private readonly SmtpClient _emailCase;

        private string subject = "OCS Credentialing System Review and Submission by the Delegate";

        private string template = NotificationTemplate.ProviderReviewNotificationTemplate;

        public ProviderReviewNotificationEmail(SmtpClient emailCase)
        {
            _emailCase = emailCase;
        }

        public async Task<CommResponseDto> SendEmailAsync(ProviderReviewNotificationRequestDto request)
        {
            ReplaceContent(template, request.Link);
            SmtpClientRequest email = new SmtpClientRequest();
            email.Body = template;
            email.Subject = subject;
            email.ToEmail = request.ToEmail;
            return await _emailCase.SendEmailAsync(email);
        }

        private void ReplaceContent(string content, string link)
        {
            template = content.Replace("[link]", link);
        }
    }
}
