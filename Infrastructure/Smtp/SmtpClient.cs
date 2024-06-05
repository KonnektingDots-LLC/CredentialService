using AutoMapper;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Security;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Infrastructure.Settings;
using MimeKit.Text;
using cred_system_back_end_app.Application.UseCase.Notifications;

namespace cred_system_back_end_app.Infrastructure.Smpt
{
    public class SmtpClient
    {

        private readonly IMapper _mapper;
        private readonly SmtpSettings _smtpSettings;
        private readonly ILogger<SmtpClient> _logger;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly SaveNotificationCase _saveNotificationCase;


        public SmtpClient(IMapper mapper, IOptions<SmtpSettings> smtpSettings, 
            ILogger<SmtpClient> logger, IConfiguration configuration, IWebHostEnvironment env,
            SaveNotificationCase saveNotificationCase)
        {
            _mapper = mapper;
            _smtpSettings = smtpSettings.Value;
            _logger = logger;
            _configuration = configuration;
            _env = env;
            if (!_env.IsDevelopment()) {
                _smtpSettings.SmtpPass = _configuration["SmtpPass"];
                _smtpSettings.Username = _configuration["SmtpUser"];
                _smtpSettings.SenderEmail = _configuration["SmtpUser"];
            }
           _saveNotificationCase = saveNotificationCase;
            
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
