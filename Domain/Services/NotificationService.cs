using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Domain.Settings;
using Microsoft.Extensions.Options;

namespace cred_system_back_end_app.Domain.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IGenericRepository<NotificationEntity, int> _notificationRepository;
        private readonly INotificationProfileCompletionDetailRepository _notificationProfileCompletionDetailRepository;
        private readonly SmtpSettings _smtpSettings;

        public NotificationService(
            IGenericRepository<NotificationEntity, int> notificationRepository,
            INotificationProfileCompletionDetailRepository notificationProfileCompletionDetailRepository,
            IOptions<SmtpSettings> smtpSettings,
            IConfiguration configuration,
            IWebHostEnvironment env)
        {
            _smtpSettings = smtpSettings.Value;
            _notificationRepository = notificationRepository;
            _notificationProfileCompletionDetailRepository = notificationProfileCompletionDetailRepository;

            if (!env.IsDevelopment())
            {
                _smtpSettings.SenderEmail = configuration["SmtpUser"];
            }
        }

        public NotificationEntity GetNotificationEntity(int resourceId, string notificationTypeId, string resourceTypeId, string receiverEmail)
        {
            return GetNotificationEntity(resourceId, notificationTypeId, resourceTypeId, receiverEmail, _smtpSettings.SenderEmail);
        }

        public NotificationEntity GetNotificationEntity(int resourceId, string notificationTypeId, string resourceTypeId, string receiverEmail, string sentBy)
        {

            return new NotificationEntity
            {
                NotificationTypeId = notificationTypeId,
                ResourceId = resourceId.ToString(),
                ResourceTypeId = resourceTypeId,
                NotificationStatus = GetNotificationStatus(receiverEmail),
                CreatedBy = sentBy,
                CreationDate = DateTime.Now,
            };
        }

        /// <summary>
        /// Send a notification.
        /// </summary>
        /// <param name="notificationEntity"></param>
        /// <param name="sendEmailAction"></param>
        /// <returns></returns>
        public async Task SendNotificationAsync(NotificationEntity notificationEntity, Func<Task<(string, string)>> sendEmailAction)
        {
            try
            {
                var (subject, body) = await sendEmailAction();

                notificationEntity.NotificationStatus.Subject = subject;
                notificationEntity.NotificationStatus.Body = body;

                await SaveNotification(notificationEntity);
            }
            catch (Exception ex)
            {
                await SaveNotification(ex, notificationEntity);
            }
        }

        /// <summary>
        /// Send a notification.
        /// </summary>
        /// <param name="notificationProfileCompletionDetailEntity"></param>
        /// <param name="sendEmailAction"></param>
        /// <returns></returns>
        public async Task SendNotificationAsync(NotificationProfileCompletionDetailEntity notificationProfileCompletionDetailEntity, Func<Task<(string, string)>> sendEmailAction)
        {
            try
            {
                await sendEmailAction();
                await _notificationProfileCompletionDetailRepository.AddAndSaveAsync(notificationProfileCompletionDetailEntity);
            }
            catch (Exception ex)
            {
                //
            }
        }

        public NotificationStatusEntity GetNotificationStatus(string receiverEmail, bool isSuccess = true)
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

        public void SetNotificationError(NotificationStatusEntity notificationStatus, string errorDetail)
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

        public async Task SaveNotification(Exception ex, NotificationEntity notification)
        {
            var errorMessage = $"{ex.Message} {(ex.InnerException == null ? "" : ex.InnerException.Message)}";

            SetNotificationError(notification.NotificationStatus, errorMessage);

            await SaveNotification(notification);
        }

        public async Task SaveNotification(NotificationEntity notificationEntity)
        {
            await _notificationRepository.AddAndSaveAsync(notificationEntity);
        }
    }
}
