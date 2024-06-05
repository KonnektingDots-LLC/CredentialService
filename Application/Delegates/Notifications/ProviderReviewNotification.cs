using MediatR;

namespace cred_system_back_end_app.Application.Delegates.Notifications
{
    public record class ProviderReviewNotification(string ToEmail) : INotification;
}
