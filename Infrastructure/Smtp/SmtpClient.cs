using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Settings;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace cred_system_back_end_app.Infrastructure.Smtp
{
    public class SmtpClient
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly ILogger<SmtpClient> _logger;

        public SmtpClient(IOptions<SmtpSettings> smtpSettings,
            ILogger<SmtpClient> logger, IConfiguration configuration, IWebHostEnvironment env)
        {
            _smtpSettings = smtpSettings.Value;
            _logger = logger;
            if (!env.IsDevelopment())
            {
                _smtpSettings.SmtpPass = configuration["SmtpPass"];
                _smtpSettings.Username = configuration["SmtpUser"];
                _smtpSettings.SenderEmail = configuration["SmtpUser"];
            }
        }

        public async Task<CommResponseDto> SendEmailAsync(SmtpClientRequest email)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
                message.To.Add(new MailboxAddress("", email.ToEmail));
                message.Subject = email.Subject;

                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = email.Body
                };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.SmtpPass);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                    return new CommResponseDto()
                    {
                        Body = email.Body,
                        Subject = email.Subject,
                        SenderEmail = _smtpSettings.SenderEmail
                    };
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors
                throw;
            }
        }


    }
}
