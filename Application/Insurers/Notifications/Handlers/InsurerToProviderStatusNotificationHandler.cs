using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.DTO.Notifications;
using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Domain.Interfaces.Emails;
using MediatR;

namespace cred_system_back_end_app.Application.Insurers.Notifications.Handlers
{
    public class InsurerToProviderStatusNotificationHandler : INotificationHandler<InsurerToProviderStatusNotification>
    {
        private readonly INotificationService _notificationService;
        private readonly IInsurerToProviderStatusEmail<NotificationEmailDto> _insurerToProviderStatusEmail;
        private readonly IConfiguration _configuration;
        private readonly IProviderService _providerService;

        public InsurerToProviderStatusNotificationHandler
        (
            IInsurerToProviderStatusEmail<NotificationEmailDto> insurerToProviderStatusEmail,
            INotificationService notificationService,
            IConfiguration configuration,
            IProviderService providerService
        )
        {
            _insurerToProviderStatusEmail = insurerToProviderStatusEmail;
            _notificationService = notificationService;
            _configuration = configuration;
            _providerService = providerService;
        }

        public async Task Handle(InsurerToProviderStatusNotification notification, CancellationToken cancellationToken)
        {
            await SendNotificationAsync(notification.ProviderId, notification.ToEmail, notification.FromEmail);
        }

        private async Task SendNotificationAsync(int providerId, string toEmail, string fromEmail)
        {
            var provider = await _providerService.GetProviderById(providerId);
            var notificationEntity = _notificationService.GetNotificationEntity
            (
                providerId,
                NotificationType.INSURER_PROVIDER_STATUS,
                ResourceType.PROVIDER,
                toEmail,
                fromEmail
            );

            NotificationEmailDto emailRequest = new()
            {
                ToEmail = toEmail,
                ProviderName = provider?.FirstName + " " + provider?.LastName,
                Link = _configuration["FeUrl"]
            };

            await _notificationService.SendNotificationAsync
            (
                notificationEntity,
                async () => await _insurerToProviderStatusEmail.SendEmailAsync(emailRequest)
            );
        }

    }
}
