using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using cred_system_back_end_app.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace cred_system_back_end_app.Application.UseCase.Notifications.NotificationManagers
{
    public class NotificationManagerBase
    {
        private NotificationEntity _notification;
        private readonly SaveNotificationCase _saveNotificationCase;
        private readonly SmtpSettings _smtpSettings;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public NotificationManagerBase(
            SaveNotificationCase saveNotificationCase,
            IOptions<SmtpSettings> smtpSettings,
            IConfiguration configuration,
            IWebHostEnvironment env)
        {
            _saveNotificationCase = saveNotificationCase;
            _smtpSettings = smtpSettings.Value;
            _env = env;
            _configuration = configuration;

            if (!_env.IsDevelopment())
            {
                _smtpSettings.SenderEmail = _configuration["SmtpUser"];
            }
        }

        protected void SetNotificationData(int resourceId, string notificationTypeId, string resourceTypeId, string receiverEmail)
        {

            _notification = new NotificationEntity
            {
                NotificationTypeId = notificationTypeId,
                ResourceId = resourceId.ToString(),
                ResourceTypeId = resourceTypeId,
                NotificationStatus = GetNotificationStatus(receiverEmail),
                CreatedBy = _smtpSettings.SenderEmail,
                CreatedDate = DateTime.Now,
            };
        }

        protected void SetNotificationData(int resourceId, string notificationTypeId, string resourceTypeId, string receiverEmail, string sentBy)
        {

            _notification = new NotificationEntity
            {
                NotificationTypeId = notificationTypeId,
                ResourceId = resourceId.ToString(),
                ResourceTypeId = resourceTypeId,
                NotificationStatus = GetNotificationStatus(receiverEmail),
                CreatedBy = sentBy,
                CreatedDate = DateTime.Now,
            };
        }

        protected async Task SendNotificationAsync(Func<Task<CommResponseDto>> sendEmailAction)
        {
            try
            {
                var emailData = await sendEmailAction();

                _notification.NotificationStatus.Subject = emailData.Subject;
                _notification.NotificationStatus.Body = emailData.Body;

                await SaveNotification(_notification);
            }
            catch (Exception ex)
            {
                await SaveNotification(ex, _notification);
            }
        }

        protected NotificationStatusEntity GetNotificationStatus(string receiverEmail, bool isSuccess = true)
        {
            return new NotificationStatusEntity
            {
                CreatedDate = DateTime.UtcNow,
                CreatedBy = _smtpSettings.SenderEmail,
                EmailTo = receiverEmail,
                EmailFrom = _smtpSettings.SenderEmail,
                IsSuccess = isSuccess,
            };
        }

        protected void SetNotificationError(NotificationStatusEntity notificationStatus, string errorDetail)
        {
            notificationStatus.IsSuccess = false;
            notificationStatus.NotificationErrors = new List<NotificationErrorEntity>()
            {
                    new NotificationErrorEntity
                    {
                        ErrorDetail = errorDetail,
                        CreatedBy = _smtpSettings.SenderEmail,
                        CreatedDate = DateTime.Now,
                    }
            };
        }

        protected async Task SaveNotification(Exception ex, NotificationEntity notification)
        {
            var errorMessage = $"{ex.Message} {(ex.InnerException == null ? "" : ex.InnerException.Message)}";

            SetNotificationError(notification.NotificationStatus, errorMessage);

            await SaveNotification(notification);
        }

        protected async Task SaveNotification(NotificationEntity notificationEntity)
        {
            await _saveNotificationCase.SaveNotification(notificationEntity);
        }
    }
}
