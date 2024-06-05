using cred_system_back_end_app.Application.Common.Constant;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Infrastructure.Smpt;
using cred_system_back_end_app.Infrastructure.Smtp.InsurerInvitationNotification.DTO;
using cred_system_back_end_app.Infrastructure.Smtp.Template;

namespace cred_system_back_end_app.Infrastructure.Smtp.InsurerInvitationNotification
{
    public class InsurerInvitationNotificationEmail
    {
        private readonly SmtpClient _emailCase;

        private string subject = "OCS Credentialing System Insurer Invitation";

        private string template = NotificationTemplate.InsurerEmployeeNotificationTemplate;
    

        public InsurerInvitationNotificationEmail(SmtpClient emailCase)
        {
            _emailCase = emailCase;
        }

        public async Task<CommResponseDto> SendEmailAsync(InsurerInvitationNotificationRequestDto request)
        {
            ReplaceContent(request.InsurerName, request.Link);
            SmtpClientRequest email = new SmtpClientRequest();
            email.Body = template;
            email.Subject = subject;
            email.ToEmail = request.ToEmail;
            return await _emailCase.SendEmailAsync(email);

        }

        private void ReplaceContent(string name, string link)
        {
            template = template.Replace("[Insurer's Name]", name);
            template = template.Replace("[link]", link);
        }
  
    }
}
