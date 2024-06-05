using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace cred_system_back_end_app.Application.UseCase.Notifications.NotificationManagers
{
    public class DelegateSubmitNotificationManager : NotificationManagerBase
    {
        private readonly ProviderSubmitToDelegateCase _providerSubmitToDelegateCase;

        public DelegateSubmitNotificationManager
        (
            ProviderSubmitToDelegateCase providerSubmitToDelegateCase,
            SaveNotificationCase saveNotificationCase,
            IOptions<SmtpSettings> smtpSettings,
            IConfiguration configuration,
            IWebHostEnvironment env
        ) : base(saveNotificationCase, smtpSettings, configuration, env)
        {
            _providerSubmitToDelegateCase = providerSubmitToDelegateCase;
        }

        public async Task SendNotificationAsync(int providerId, string email) 
        {
            SetNotificationData
            (
                providerId, 
                NotificationType.PROVIDER_DELEGATE_SUB,
                ResourceType.PROVIDER,
                email
            );

            await SendNotificationAsync
            (
                async () => await _providerSubmitToDelegateCase
                .SendProviderSubmitToDelegateEmailAsync(providerId, email)
            );
        }
    }
}
