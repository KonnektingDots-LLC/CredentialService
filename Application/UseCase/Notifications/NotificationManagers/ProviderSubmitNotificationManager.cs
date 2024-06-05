using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace cred_system_back_end_app.Application.UseCase.Notifications.NotificationManagers
{
    public class ProviderSubmitNotificationManager : NotificationManagerBase
    {
        private readonly ProviderSubmitCase _providerSubmitCase;

        public ProviderSubmitNotificationManager
        (
            ProviderSubmitCase providerSubmitCase,
            SaveNotificationCase saveNotificationCase,
            IOptions<SmtpSettings> smtpSettings,
            IConfiguration configuration,
            IWebHostEnvironment env
        ) : base(saveNotificationCase, smtpSettings, configuration, env)
        {
            _providerSubmitCase = providerSubmitCase;
        }

        public async Task SendNotificationAsync(int providerId, string email) 
        {
            SetNotificationData
            (
                providerId, 
                NotificationType.PROVIDER_SUB,
                ResourceType.PROVIDER,
                email
            );

            await SendNotificationAsync
            (
                async () => await _providerSubmitCase.SendProviderSubmitEmailAsync(email)
            );
        }
    }
}
