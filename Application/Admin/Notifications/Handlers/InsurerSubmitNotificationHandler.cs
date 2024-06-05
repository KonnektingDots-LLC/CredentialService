using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.DTO.Notifications;
using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Domain.Interfaces.Emails;
using MediatR;

namespace cred_system_back_end_app.Application.Admin.Notifications.Handlers
{
    public class InsurerSubmitNotificationHandler : INotificationHandler<InsurerSubmitNotification>
    {
        private readonly IProviderSubmitToInsurerNotificationEmail<NotificationEmailDto> _providerSubmitToInsurerNotificationEmail;
        private readonly IProviderService _providerService;
        private readonly INotificationService _notificationService;
        private readonly IConfiguration _configuration;

        public InsurerSubmitNotificationHandler
        (
            IProviderSubmitToInsurerNotificationEmail<NotificationEmailDto> providerSubmitToInsurerNotificationEmail,
            IProviderService providerService,
            INotificationService notificationService,
            IConfiguration configuration
        )
        {
            _providerSubmitToInsurerNotificationEmail = providerSubmitToInsurerNotificationEmail;
            _notificationService = notificationService;
            _providerService = providerService;
            _configuration = configuration;
        }

        public async Task Handle(InsurerSubmitNotification notification, CancellationToken cancellationToken)
        {
            await SendNotificationAsync(notification.ProviderId, notification.ToEmail);
        }

        private async Task SendNotificationAsync(int providerId, string toEmail)
        {
            var provider = await _providerService.GetProviderById(providerId);
            var notificationEntity = _notificationService.GetNotificationEntity
            (
                providerId,
                NotificationType.PROVIDER_INSURER_SUB,
                ResourceType.PROVIDER,
                toEmail
            );

            NotificationEmailDto emailRequest = new NotificationEmailDto
            {
                ToEmail = toEmail,
                ProviderName = provider?.FirstName + " " + provider?.LastName,
                Link = _configuration["FeUrl"] + "?event=IRD&providerId=" + provider?.Id,
                ProviderId = provider.Id
            };

            await _notificationService.SendNotificationAsync
            (
                notificationEntity,
                async () => await _providerSubmitToInsurerNotificationEmail
                .SendEmailAsync(emailRequest)
            );
        }
    }
}
