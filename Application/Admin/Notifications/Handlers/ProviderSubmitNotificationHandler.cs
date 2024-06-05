using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.DTO.Notifications;
using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Domain.Interfaces.Emails;
using MediatR;

namespace cred_system_back_end_app.Application.Admin.Notifications.Handlers
{
    public class ProviderSubmitNotificationHandler : INotificationHandler<ProviderSubmitNotification>
    {
        private readonly INotificationService _notificationService;
        private readonly IProviderSubmitNotificationEmail<NotificationEmailDto> _providerSubmitNotificationEmail;

        public ProviderSubmitNotificationHandler
        (
            IProviderSubmitNotificationEmail<NotificationEmailDto> providerSubmitNotificationEmail,
            INotificationService notificationService
        )
        {
            _providerSubmitNotificationEmail = providerSubmitNotificationEmail;
            _notificationService = notificationService;
        }

        public async Task Handle(ProviderSubmitNotification notification, CancellationToken cancellationToken)
        {
            await SendNotificationAsync(notification.ProviderId, notification.ToEmail);
        }

        public async Task SendNotificationAsync(int providerId, string toEmail)
        {
            var notificationEntity = _notificationService.GetNotificationEntity
            (
                providerId,
                NotificationType.PROVIDER_SUB,
                ResourceType.PROVIDER,
                toEmail
            );

            await _notificationService.SendNotificationAsync
            (
                notificationEntity,
                async () => await _providerSubmitNotificationEmail.SendEmailAsync(new NotificationEmailDto(toEmail))
            );
        }
    }
}
