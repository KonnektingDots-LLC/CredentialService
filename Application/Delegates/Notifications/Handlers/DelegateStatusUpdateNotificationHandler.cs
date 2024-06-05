using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Application.DTO.Notifications;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Domain.Interfaces.Emails;
using MediatR;

namespace cred_system_back_end_app.Application.Delegates.Notifications.Handlers
{
    public class DelegateStatusUpdateNotificationHandler : INotificationHandler<DelegateStatusUpdateNotification>
    {
        private readonly INotificationService _notificationService;
        private readonly IDelegateStatusUpdateEmail<NotificationEmailDto> _delegateStatusUpdateEmail;

        public DelegateStatusUpdateNotificationHandler(INotificationService notificationService, IDelegateStatusUpdateEmail<NotificationEmailDto> delegateStatusUpdateEmail)
        {
            _delegateStatusUpdateEmail = delegateStatusUpdateEmail;
            _notificationService = notificationService;
        }

        public Task Handle(DelegateStatusUpdateNotification notification, CancellationToken cancellationToken)
        {
            SendNotification(notification.Delegate.Email, notification.Provider);

            return Task.CompletedTask;
        }

        private void SendNotification(string toEmail, ProviderEntity provider)
        {
            _ = Task.Run(async () =>
            {
                var notificationEntity = _notificationService.GetNotificationEntity
                (
                    provider.Id,
                    NotificationType.DELEGATE_STATUS_UPDATE,
                    ResourceType.PROVIDER,
                    toEmail,
                    provider.Email
                );

                var notificationEmailDto = new NotificationEmailDto
                {
                    ToEmail = toEmail,
                    ProviderName = provider.GetFullName()
                };

                await _notificationService.SendNotificationAsync
                (
                    notificationEntity,
                    async () => await _delegateStatusUpdateEmail
                    .SendEmailAsync(notificationEmailDto)
                );
            });
        }

    }
}
