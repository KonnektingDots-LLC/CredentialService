using cred_system_back_end_app.Domain.Entities;
using MediatR;

namespace cred_system_back_end_app.Application.Insurers.Notifications
{
    public record class InsurerInvitationNotification(string? ToEmail, InsurerAdminEntity? InsurerAdmin) : INotification;
}
