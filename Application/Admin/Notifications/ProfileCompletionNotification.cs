using MediatR;

namespace cred_system_back_end_app.Application.Admin.Notifications
{
    public record class ProfileCompletionNotification(string ToEmail) : INotification;
}
