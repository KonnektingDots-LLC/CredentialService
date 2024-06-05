using cred_system_back_end_app.Application.DTO.Notifications;
using cred_system_back_end_app.Application.DTO.Responses;

namespace cred_system_back_end_app.Infrastructure.Smtp
{
    public abstract class EmailBase
    {
        private readonly SmtpClient _smtpClient;
        protected readonly string _subject;
        protected string _template;

        protected EmailBase(SmtpClient smtpClient, string subject, string template)
        {
            _smtpClient = smtpClient;
            _subject = subject;
            _template = template;
        }

        public virtual string GetBody(NotificationEmailDto emailDto)
        { return _template; }

        public virtual async Task<(string, string)> SendEmailAsync(NotificationEmailDto emailDto)
        {
            var email = new SmtpClientRequest()
            {
                Body = GetBody(emailDto),
                Subject = _subject,
                ToEmail = emailDto.ToEmail,
            };

            CommResponseDto responseDto = await _smtpClient.SendEmailAsync(email);

            return (responseDto.Subject, responseDto.Body);
        }
    }
}
