using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Infrastructure.Smpt;

namespace cred_system_back_end_app.Infrastructure.Smtp
{
    public abstract class EmailBase
    {
        private readonly SmtpClient _emailCase;

        protected string Subject;

        protected string Template;

        public EmailBase(SmtpClient emailCase)
        {
            _emailCase = emailCase;
        }

        public abstract string GetBody();

        public async Task<CommResponseDto> SendEmailAsync(string ToEmail)
        {
            var email = new SmtpClientRequest()
            {
                Body = GetBody(),
                Subject = Subject,
                ToEmail = ToEmail,
            };

            return await _emailCase.SendEmailAsync(email);
        }
    }
}
