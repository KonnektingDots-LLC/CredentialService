using cred_system_back_end_app.Application.DTO.Notifications;
using cred_system_back_end_app.Domain.Interfaces.Emails;
using MediatR;

namespace cred_system_back_end_app.Application.Delegates.Notifications.Handlers
{
    public class ProviderReviewNotificationHandler : INotificationHandler<ProviderReviewNotification>
    {
        private readonly IProviderReviewNotificationEmail<NotificationEmailDto> _providerNotificationEmail;
        private readonly IConfiguration _configuration;

        public ProviderReviewNotificationHandler(IProviderReviewNotificationEmail<NotificationEmailDto> providerNotificationEmail,
            IConfiguration configuration)
        {
            _providerNotificationEmail = providerNotificationEmail;
            _configuration = configuration;
        }

        public async Task Handle(ProviderReviewNotification notification, CancellationToken cancellationToken)
        {
            await SendEmailAsync(notification.ToEmail);
        }

        private async Task SendEmailAsync(string toEmail)
        {

            NotificationEmailDto emailRequest = new NotificationEmailDto
            {
                ToEmail = toEmail,
                Link = _configuration["FeUrl"] + "?event=RP"
            };

            await _providerNotificationEmail.SendEmailAsync(emailRequest);
        }
    }
}
