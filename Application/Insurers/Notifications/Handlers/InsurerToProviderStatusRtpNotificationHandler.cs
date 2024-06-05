using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.DTO.Notifications;
using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Domain.Interfaces.Emails;
using MediatR;

namespace cred_system_back_end_app.Application.Insurers.Notifications.Handlers
{
    public class InsurerToProviderStatusRtpNotificationHandler : INotificationHandler<InsurerToProviderStatusRtpNotification>
    {
        private readonly IInsurerToProviderStatusRtpEmail<NotificationEmailDto> _insurerToProviderStatusRtpEmail;
        private readonly INotificationService _notificationService;

        public InsurerToProviderStatusRtpNotificationHandler
        (
            INotificationService notificationService,
            IInsurerToProviderStatusRtpEmail<NotificationEmailDto> insurerToProviderStatusRtpEmail
        )
        {
            _notificationService = notificationService;
            _insurerToProviderStatusRtpEmail = insurerToProviderStatusRtpEmail;
        }

        public async Task Handle(InsurerToProviderStatusRtpNotification notification, CancellationToken cancellationToken)
        {
            await SendNotificationAsync(notification.ProviderId, notification.ToEmail, notification.FromEmail);
        }

        private async Task SendNotificationAsync(int providerId, string toEmail, string fromEmail)
        {
            var notificationEntity = _notificationService.GetNotificationEntity
            (
                providerId,
                NotificationType.INSURER_PROVIDER_STATUS,
                ResourceType.PROVIDER,
                toEmail,
                fromEmail
            );

            await _notificationService.SendNotificationAsync
            (
                notificationEntity,
                async () => await _insurerToProviderStatusRtpEmail.SendEmailAsync(new NotificationEmailDto(toEmail))
            );
        }
    }
}
