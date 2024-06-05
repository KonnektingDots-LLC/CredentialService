using MediatR;

namespace cred_system_back_end_app.Application.Admin.Notifications
{
    public record class ProviderSubmitNotification(int ProviderId, string ToEmail) : INotification;
}
