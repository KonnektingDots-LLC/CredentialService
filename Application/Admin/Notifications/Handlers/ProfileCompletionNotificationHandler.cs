using cred_system_back_end_app.Application.DTO.Notifications;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Domain.Interfaces.Emails;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using MediatR;

namespace cred_system_back_end_app.Application.Admin.Notifications.Handlers
{
    public class ProfileCompletionNotificationHandler : INotificationHandler<ProfileCompletionNotification>
    {
        private readonly INotificationService _notificationService;
        private readonly IProfileCompletionNotificationEmail<NotificationEmailDto> _profileCompletionNotificationEmail;
        private readonly INotificationProfileCompletionDetailRepository _notificationProfileCompletionDetailRepository;

        public ProfileCompletionNotificationHandler(INotificationService notificationService,
            IProfileCompletionNotificationEmail<NotificationEmailDto> profileCompletionNotificationEmail,
            INotificationProfileCompletionDetailRepository notificationProfileCompletionDetailRepository)
        {
            _profileCompletionNotificationEmail = profileCompletionNotificationEmail;
            _notificationService = notificationService;
            _notificationProfileCompletionDetailRepository = notificationProfileCompletionDetailRepository;
        }

        public async Task Handle(ProfileCompletionNotification notification, CancellationToken cancellationToken)
        {
            var notificationProfile = await _notificationProfileCompletionDetailRepository.GetByEmailAsync(notification.ToEmail);
            if (notificationProfile != null && notificationProfile.Sent)
            {
                return;
            }

            await SendNotification(notification.ToEmail);
        }

        private Task SendNotification(string toEmail)
        {
            _ = Task.Run(async () =>
            {
                var notificationProfileCompletionDetailEntity = new NotificationProfileCompletionDetailEntity
                {
                    Email = toEmail,
                    Sent = true,
                };

                await _notificationService.SendNotificationAsync
                (
                    notificationProfileCompletionDetailEntity,
                    async () => await _profileCompletionNotificationEmail
                    .SendEmailAsync(new NotificationEmailDto(toEmail))
                );
            });

            return Task.CompletedTask;
        }
    }
}
