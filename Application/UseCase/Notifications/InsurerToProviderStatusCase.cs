using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Application.CRUD.Provider;
using cred_system_back_end_app.Infrastructure.Smtp.InsurerToProviderStatusNotification;
using cred_system_back_end_app.Infrastructure.Smtp.ProviderSubmitNotification;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace cred_system_back_end_app.Application.UseCase.Notifications
{
    public class InsurerToProviderStatusCase
    {
        private readonly InsurerToProviderStatusEmail _insurerToProviderStatusEmail;
        private readonly ProviderRepository _providerRepo;
        private readonly IConfiguration _configuration;

        public InsurerToProviderStatusCase(InsurerToProviderStatusEmail insurerToProviderStatusEmail,
            ProviderRepository providerRepo,
            IConfiguration configuration)
        {
            _insurerToProviderStatusEmail = insurerToProviderStatusEmail;
            _providerRepo = providerRepo;
            _configuration = configuration;
        }

        public async Task<CommResponseDto> SendInsurerToProviderStatusEmailAsync(int providerId, string providerEmail)
        {
            var provider = _providerRepo.GetProviderEntityById(providerId);
            _insurerToProviderStatusEmail.ProviderName = provider.FirstName + " " + provider.LastName;
            _insurerToProviderStatusEmail.WebsiteLink = _configuration["FeUrl"];
            return await _insurerToProviderStatusEmail.SendEmailAsync(providerEmail);
        }
    }
}
