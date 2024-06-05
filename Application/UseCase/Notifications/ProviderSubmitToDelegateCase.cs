using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Application.UseCase.Notifications.DTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.Smtp.ProviderSubmitToDelegateNotification;

namespace cred_system_back_end_app.Application.UseCase.Notifications
{
    public class ProviderSubmitToDelegateCase
    {
        private readonly DbContextEntity _contextEntity;
        private readonly ProviderSubmitToDelegateNotificationEmail _ProviderSubmitToDelegateNotificationEmail;
        private readonly IConfiguration _configuration;

        public ProviderSubmitToDelegateCase(DbContextEntity contextEntity,
            ProviderSubmitToDelegateNotificationEmail ProviderSubmitToDelegateNotificationEmail,
            IConfiguration configuration)
        {
            _contextEntity = contextEntity;
            _ProviderSubmitToDelegateNotificationEmail = ProviderSubmitToDelegateNotificationEmail;
            _configuration = configuration;
        }

        public async Task<CommResponseDto> SendProviderSubmitToDelegateEmailAsync(ProviderSubmitToDelegateInfoDto request)
        {

            var provider = _contextEntity.Provider.FirstOrDefault(p => p.Id == request.ProviderId);
            if (provider == null)
            {

                throw new EntityNotFoundException();
            }

            ProviderSubmitToDelegateRequestDto emailRequest = new ProviderSubmitToDelegateRequestDto
            {
                EmailTo = request.Email,
                ProviderName = provider.FirstName + " " + provider.LastName,
            };

            return await _ProviderSubmitToDelegateNotificationEmail.SendEmailAsync(emailRequest);
        }

        public async Task<CommResponseDto> SendProviderSubmitToDelegateEmailAsync(int providerId, string emailTo) 
        {
            var providerSubmitToDelegateInfoDto = new ProviderSubmitToDelegateInfoDto
            {
                ProviderId = providerId,
                Email = emailTo,
            };

            return await SendProviderSubmitToDelegateEmailAsync(providerSubmitToDelegateInfoDto);
        }
    }
}
