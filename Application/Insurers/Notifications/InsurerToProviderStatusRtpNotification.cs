using MediatR;

namespace cred_system_back_end_app.Application.Insurers.Notifications
{
    public record class InsurerToProviderStatusRtpNotification(int ProviderId, string ToEmail, string FromEmail) : INotification;
}
