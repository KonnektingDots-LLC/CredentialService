using cred_system_back_end_app.Application.Common.Constant;
using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Infrastructure.Smpt;
using cred_system_back_end_app.Infrastructure.Smtp.Template;

namespace cred_system_back_end_app.Infrastructure.Smtp.DelegateInvitationNotification
{
    public class DelegateInvitationNotificationEmail
    {
        private readonly SmtpClient _emailCase;

        private string subject = "OCS Credentialing System Delegate Invitation";

        private string template = NotificationTemplate.DelegateInvitationNotificationTemplate;

        public DelegateInvitationNotificationEmail(SmtpClient emailCase)
        {
            _emailCase = emailCase;
        }

        public async Task<CommResponseDto> SendEmailAsync(DelegateInvitationNotificationRequestDto request)
        {
            ReplaceContent(request.ProviderName, request.Link);
            SmtpClientRequest email = new SmtpClientRequest();
            email.Body = template;
            email.Subject = subject;
            email.ToEmail = request.ToEmail;
            
            return await _emailCase.SendEmailAsync(email);

        }

        private void ReplaceContent(string name, string link)
        {
            template = template.Replace("[Provider's Name]", name);
            template = template.Replace("[link]", link);
        }
    }
}
