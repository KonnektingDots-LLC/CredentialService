using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.UseCase.Notifications;
using cred_system_back_end_app.Application.UseCase.Notifications.NotificationManagers;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using cred_system_back_end_app.Infrastructure.Settings;
using cred_system_back_end_app.Infrastructure.Smtp.ProviderDelegateUpdateNotification;
using Microsoft.Extensions.Options;

namespace cred_system_back_end_app.Application.UseCase.Delegate
{
    public class ProviderDelegateStatusNotificationManager : NotificationManagerBase
    {
        private readonly ProviderDelegateStatusUpdate _providerDelegateStatusUpdateEmail;

        public ProviderDelegateStatusNotificationManager(
            SaveNotificationCase saveNotificationCase,
            ProviderDelegateStatusUpdate providerDelegateStatusUpdateEmail,
            IOptions<SmtpSettings> smptSettings,
            IConfiguration configuration,
            IWebHostEnvironment env)
            : base(saveNotificationCase, smptSettings, configuration, env)
        {
            _providerDelegateStatusUpdateEmail = providerDelegateStatusUpdateEmail;
        }

        public async Task SendNotification(string delegateName, ProviderEntity provider)
        {
            var notification = new NotificationEntity
            {
                ResourceId = provider.Id.ToString(),
                ResourceTypeId = ResourceType.PROVIDER,
                NotificationTypeId = NotificationType.DELEGATE_STATUS_UPDATE,
                NotificationStatus = GetNotificationStatus(provider.Email),
                CreatedBy = provider.Email,
                CreatedDate = DateTime.UtcNow
            };

            try
            {
                _providerDelegateStatusUpdateEmail.DelegateName = delegateName;

                var emailData = await _providerDelegateStatusUpdateEmail.SendEmailAsync(provider.Email);

                notification.NotificationStatus.Subject = emailData.Subject;
                notification.NotificationStatus.Body = emailData.Body;

                await SaveNotification(notification);
            }
            catch (Exception ex)
            {
                await SaveNotification(ex, notification);
            }
        }
    }
}
