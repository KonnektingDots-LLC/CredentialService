using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace cred_system_back_end_app.Application.UseCase.Notifications.NotificationManagers
{
    public class InsurerSubmitNotificationManager : NotificationManagerBase
    {
        private readonly ProviderSubmitToInsurerCase _providerSubmitToInsurerCase;

        public InsurerSubmitNotificationManager
        (
            ProviderSubmitToInsurerCase providerSubmitToInsurerCase,
            SaveNotificationCase saveNotificationCase,
            IOptions<SmtpSettings> smtpSettings,
            IConfiguration configuration,
            IWebHostEnvironment env
        ) : base(saveNotificationCase, smtpSettings, configuration, env)
        {
            _providerSubmitToInsurerCase = providerSubmitToInsurerCase;
        }

        public async Task SendNotificationAsync(int providerId, string email) 
        {
            SetNotificationData
            (
                providerId, 
                NotificationType.PROVIDER_INSURER_SUB,
                ResourceType.PROVIDER,
                email
            );

            await SendNotificationAsync
            (
                async () => await _providerSubmitToInsurerCase
                .SendProviderSubmitToInsurerEmailAsync(email, providerId)
            );
        }
    }
}
