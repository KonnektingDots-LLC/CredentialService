using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.DTO.Notifications;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Domain.Interfaces.Emails;
using MediatR;

namespace cred_system_back_end_app.Application.Providers.Notifications.Handlers
{
    public class ProviderDelegateStatusNotificationHandler : INotificationHandler<ProviderDelegateStatusNotification>
    {
        private readonly INotificationService _notificationService;
        private readonly IProviderDelegateStatusUpdateEmail<NotificationEmailDto> _providerDelegateStatusUpdateEmail;

        public ProviderDelegateStatusNotificationHandler(
            INotificationService notificationService,
            IProviderDelegateStatusUpdateEmail<NotificationEmailDto> providerDelegateStatusUpdateEmail)
        {
            _providerDelegateStatusUpdateEmail = providerDelegateStatusUpdateEmail;
            _notificationService = notificationService;
        }

        public Task Handle(ProviderDelegateStatusNotification notification, CancellationToken cancellationToken)
        {
            SendNotification(notification.Delegate.FullName, notification.Provider);

            return Task.CompletedTask;
        }

        public void SendNotification(string delegateName, ProviderEntity provider)
        {
            _ = Task.Run(async () =>
            {
                var notificationEntity = _notificationService.GetNotificationEntity
                (
                    provider.Id,
                    NotificationType.DELEGATE_STATUS_UPDATE,
                    ResourceType.PROVIDER,
                    provider.Email,
                    provider.Email
                );

                var notificationEmailDto = new NotificationEmailDto
                {
                    DelegateName = delegateName,
                    ToEmail = provider.Email
                };

                await _notificationService.SendNotificationAsync
                (
                    notificationEntity,
                    async () => await _providerDelegateStatusUpdateEmail
                    .SendEmailAsync(notificationEmailDto)
                );
            });
        }
    }
}
