using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Application.UseCase.Notifications;
using cred_system_back_end_app.Application.UseCase.Notifications.NotificationManagers;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using cred_system_back_end_app.Infrastructure.Settings;
using cred_system_back_end_app.Infrastructure.Smtp.DelegateStatusUpdateNotification;
using cred_system_back_end_app.Infrastructure.Smtp.ProviderDelegateUpdateNotification;
using Microsoft.Extensions.Options;

namespace cred_system_back_end_app.Application.UseCase.Delegate
{
    public class DelegateStatusUpdateNotificationManager : NotificationManagerBase
    {
        private readonly DelegateStatusUpdateEmail _delegateStatusUpdateEmail;

        public DelegateStatusUpdateNotificationManager(
            SaveNotificationCase saveNotificationCase,
            DelegateStatusUpdateEmail delegateStatusUpdateEmail,
            IOptions<SmtpSettings> smptSettings,
            IConfiguration configuration,
            IWebHostEnvironment env)
            : base(saveNotificationCase, smptSettings, configuration, env)
        {
            _delegateStatusUpdateEmail = delegateStatusUpdateEmail;
        }

        public async Task SendNotification(string toEmail, ProviderEntity provider)
        {
            var notification = new NotificationEntity
            {
                ResourceId = provider.Id.ToString(),
                ResourceTypeId = ResourceType.PROVIDER,
                NotificationTypeId = NotificationType.DELEGATE_STATUS_UPDATE,
                NotificationStatus = GetNotificationStatus(toEmail),
                CreatedBy = provider.Email,
                CreatedDate = DateTime.UtcNow                
            };

            try 
            {
                _delegateStatusUpdateEmail.ProviderName = provider.GetFullName();

                var emailData = await _delegateStatusUpdateEmail.SendEmailAsync(toEmail);

                notification.NotificationStatus.Subject = emailData.Subject;
                notification.NotificationStatus.Body = emailData.Body;

                await SaveNotification(notification);
            }
            catch(Exception ex)
            {
                await SaveNotification(ex, notification);
            }
        }

    }
}
