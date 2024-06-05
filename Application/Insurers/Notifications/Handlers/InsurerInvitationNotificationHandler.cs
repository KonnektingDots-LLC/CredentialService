using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.DTO.Notifications;
using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Domain.Interfaces.Emails;
using MediatR;

namespace cred_system_back_end_app.Application.Insurers.Notifications.Handlers
{
    public class InsurerInvitationNotificationHandler : INotificationHandler<InsurerInvitationNotification>
    {
        private readonly INotificationService _notificationService;
        private readonly IConfiguration _configuration;
        private readonly IInsurerInvitationNotificationEmail<NotificationEmailDto> _insurerNotificationEmail;

        public InsurerInvitationNotificationHandler(INotificationService notificationService, IConfiguration configuration, IInsurerInvitationNotificationEmail<NotificationEmailDto> insurerNotificationEmail)
        {
            _notificationService = notificationService;
            _configuration = configuration;
            _insurerNotificationEmail = insurerNotificationEmail;
        }

        public async Task Handle(InsurerInvitationNotification notification, CancellationToken cancellationToken)
        {
            await SendNotificationAsync(notification.InsurerAdmin.Id, notification.ToEmail, notification.InsurerAdmin.Name);
        }

        private async Task SendNotificationAsync(int insurerAdminId, string? toEmail, string? insurerName)
        {
            var notificationEntity = _notificationService.GetNotificationEntity
            (
                insurerAdminId,
                NotificationType.INSURER_INV,
                ResourceType.ADMIN_INSURER,
                toEmail
            );

            NotificationEmailDto emailRequest = new()
            {
                ToEmail = toEmail,
                InsurerName = insurerName,
                Link = _configuration["FeUrl"] + "?event=II&email=" + toEmail,
            };

            await _notificationService.SendNotificationAsync
            (
                notificationEntity,
                async () => await _insurerNotificationEmail.SendEmailAsync(emailRequest)
            );
        }
    }
}
