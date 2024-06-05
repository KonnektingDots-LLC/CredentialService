using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.DTO.Notifications;
using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Domain.Interfaces.Emails;
using MediatR;

namespace cred_system_back_end_app.Application.Admin.Notifications.Handlers
{
    public class DelegateSubmitNotificationHandler : INotificationHandler<DelegateSubmitNotification>
    {
        private readonly IProviderService _providerService;
        private readonly IProviderSubmitToDelegateNotificationEmail<NotificationEmailDto> _providerSubmitToDelegateNotificationEmail;
        private readonly INotificationService _notificationService;

        public DelegateSubmitNotificationHandler
        (
            IProviderSubmitToDelegateNotificationEmail<NotificationEmailDto> providerSubmitToDelegateNotificationEmail,
            INotificationService notificationService,
            IProviderService providerService
        )
        {
            _providerSubmitToDelegateNotificationEmail = providerSubmitToDelegateNotificationEmail;
            _notificationService = notificationService;
            _providerService = providerService;
        }

        public async Task Handle(DelegateSubmitNotification notification, CancellationToken cancellationToken)
        {
            await SendNotificationAsync(notification.ProviderId, notification.ToEmail);
        }

        private async Task SendNotificationAsync(int providerId, string toEmail)
        {
            var provider = await _providerService.GetProviderById(providerId);
            var notificationEntity = _notificationService.GetNotificationEntity
            (
                providerId,
                NotificationType.PROVIDER_DELEGATE_SUB,
                ResourceType.PROVIDER,
                toEmail
            );

            NotificationEmailDto emailRequest = new NotificationEmailDto
            {
                ToEmail = toEmail,
                ProviderName = provider?.FirstName + " " + provider?.LastName,
            };

            await _notificationService.SendNotificationAsync
            (
                notificationEntity,
                async () => await _providerSubmitToDelegateNotificationEmail.SendEmailAsync(emailRequest)
            );
        }
    }
}
