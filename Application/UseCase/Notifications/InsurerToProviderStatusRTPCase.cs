using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Infrastructure.Smtp.InsurerToProviderStatusNotification;
using cred_system_back_end_app.Infrastructure.Smtp.InsurerToProviderStatusRTPNotification;

namespace cred_system_back_end_app.Application.UseCase.Notifications
{
    public class InsurerToProviderStatusRTPCase
    {
        private readonly InsurerToProviderStatusRTPEmail _insurerToProviderStatusRTPEmail;

        public InsurerToProviderStatusRTPCase(InsurerToProviderStatusRTPEmail insurerToProviderStatusRTPEmail)
        {
            _insurerToProviderStatusRTPEmail = insurerToProviderStatusRTPEmail;
        }

        public async Task<CommResponseDto> SendInsurerToProviderStatusEmailAsync(string providerEmail)
        {
            return await _insurerToProviderStatusRTPEmail.SendEmailAsync(providerEmail);
        }
    }
}
