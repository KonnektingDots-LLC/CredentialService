using AutoMapper;
using cred_system_back_end_app.Application.UseCase.Notifications.DTO;
using cred_system_back_end_app.Infrastructure.Smtp.ProviderReviewNotification;

namespace cred_system_back_end_app.Application.UseCase.Notifications
{
    public class ProviderReviewCase
    {
        private readonly ProviderReviewNotificationEmail _providerNotificationEmail;
        private readonly IConfiguration _configuration;

        public ProviderReviewCase(ProviderReviewNotificationEmail providerNotificationEmail, 
            IConfiguration configuration)
        {
            _providerNotificationEmail = providerNotificationEmail;
            _configuration = configuration;
        }

        public async Task SendEmailAsync(ProviderReviewInfoDto request)
        {

            ProviderReviewNotificationRequestDto emailRequest = new ProviderReviewNotificationRequestDto
            {
                ToEmail = request.Email,
                Link = _configuration["FeUrl"]+"?event=RP"
            };

            await _providerNotificationEmail.SendEmailAsync(emailRequest);
        }
    }
}
