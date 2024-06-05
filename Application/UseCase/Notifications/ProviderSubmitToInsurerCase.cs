using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Application.CRUD.Provider;
using cred_system_back_end_app.Application.UseCase.Notifications.DTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.Smtp.DelegateInvitationNotification;
using cred_system_back_end_app.Infrastructure.Smtp.ProviderSubmitToInsurerNotification;

namespace cred_system_back_end_app.Application.UseCase.Notifications
{
    public class ProviderSubmitToInsurerCase
    {
        private readonly DbContextEntity _contextEntity;
        private readonly ProviderSubmitToInsurerNotificationEmail _providerSubmitToInsurerNotificationEmail;
        private readonly ProviderRepository _providerRepo;
        private readonly IConfiguration _configuration;

        public ProviderSubmitToInsurerCase(
            ProviderSubmitToInsurerNotificationEmail providerSubmitToInsurerNotificationEmail,
            ProviderRepository providerRepository,
            IConfiguration configuration)
        {
            _providerSubmitToInsurerNotificationEmail = providerSubmitToInsurerNotificationEmail;
            _providerRepo = providerRepository;
            _configuration = configuration;
        }

        public async Task SendProviderSubmitToInsurerEmailAsync(ProviderSubmitToInsurerInfoDto request)
        {

            var provider = _providerRepo.GetProviderEntityById(request.ProviderId);

            if (provider == null)
            {
                throw new EntityNotFoundException();
            }

            ProviderSubmitToInsurerRequestDto emailRequest = new ProviderSubmitToInsurerRequestDto
            {
                EmailTo = request.Email,
                ProviderName = provider.FirstName + " " + provider.LastName,
                Link = _configuration["FeUrl"] + "?event=IRD&providerId=" + provider.Id,
                ProviderId = provider.Id
            };

            await _providerSubmitToInsurerNotificationEmail.SendEmailAsync(emailRequest);
        }

        public async Task<CommResponseDto> SendProviderSubmitToInsurerEmailAsync(string emailTo, int providerId)
        {
            var provider = _providerRepo.GetProviderEntityById(providerId);

            if (provider == null)
            {
                throw new EntityNotFoundException();
            }

            ProviderSubmitToInsurerRequestDto emailRequest = new ProviderSubmitToInsurerRequestDto
            {
                EmailTo = emailTo,
                ProviderName = provider.FirstName + " " + provider.LastName,
                Link = _configuration["FeUrl"] + "?event=IRD&providerId=" + provider.Id,
                ProviderId = provider.Id
            };

            return await _providerSubmitToInsurerNotificationEmail.SendEmailAsync(emailRequest);
        }
    }
}
