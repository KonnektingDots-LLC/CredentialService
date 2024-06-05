using cred_system_back_end_app.Domain.Entities;
using MediatR;

namespace cred_system_back_end_app.Application.Delegates.Notifications
{
    public record class DelegateStatusUpdateNotification(ProviderEntity Provider, DelegateEntity Delegate) : INotification;
}
