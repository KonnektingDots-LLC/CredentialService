using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.DTO.Notifications;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Domain.Interfaces.Emails;
using MediatR;

namespace cred_system_back_end_app.Application.Providers.Notifications.Handlers
{
    public class ProviderDelegateInviteNotificationHandler : INotificationHandler<ProviderDelegateInviteNotification>
    {
        private readonly INotificationService _notificationService;
        private readonly IConfiguration _configuration;
        private readonly IDelegateInvitationNotificationEmail<NotificationEmailDto> _providerDelegateInviteEmail;

        public ProviderDelegateInviteNotificationHandler(
            INotificationService notificationService,
            IDelegateInvitationNotificationEmail<NotificationEmailDto> providerDelegateInviteEmail,
            IConfiguration configuration)
        {
            _providerDelegateInviteEmail = providerDelegateInviteEmail;
            _notificationService = notificationService;
            _configuration = configuration;
        }

        public async Task Handle(ProviderDelegateInviteNotification notification, CancellationToken cancellationToken)
        {
            await SendNotification(notification.DelegateEmail, notification.Provider);
        }

        private async Task SendNotification(string delegateEmail, ProviderEntity provider)
        {

            var notificationEntity = _notificationService.GetNotificationEntity
            (
                provider.Id,
                NotificationType.DELEGATE_INV,
                ResourceType.PROVIDER,
                provider.Email,
                provider.Email
            );

            NotificationEmailDto emailRequest = new()
            {
                ToEmail = delegateEmail,
                ProviderName = provider.FirstName + " " + provider.LastName,
                Link = _configuration["FeUrl"] + "?event=DIBP&providerId=" + provider.Id + "&email=" + delegateEmail,
                ProviderId = provider.Id
            };

            await _notificationService.SendNotificationAsync
            (
                notificationEntity,
                async () => await _providerDelegateInviteEmail
                .SendEmailAsync(emailRequest)
            );
        }
    }
}
