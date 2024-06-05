using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Application.UseCase.Notifications.DTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.Smtp.ProviderSubmitNotification;

namespace cred_system_back_end_app.Application.UseCase.Notifications
{
    public class ProviderSubmitCase
    {
        private readonly DbContextEntity _contextEntity;
        private readonly ProviderSubmitNotificationEmail _providerSubmitNotificationEmail;
        private readonly IConfiguration _configuration;

        public ProviderSubmitCase(DbContextEntity contextEntity,
            ProviderSubmitNotificationEmail providerSubmitNotificationEmail,
            IConfiguration configuration)
        {
            _contextEntity = contextEntity;
            _providerSubmitNotificationEmail = providerSubmitNotificationEmail;
            _configuration = configuration;
        }

        public async Task<CommResponseDto> SendProviderSubmitEmailAsync(string providerEmail)
        {

            ProviderSubmitRequestDto emailRequest = new ProviderSubmitRequestDto
            {
                EmailTo = providerEmail,
            };

            return await _providerSubmitNotificationEmail.SendEmailAsync(emailRequest);
        }
    }
}
