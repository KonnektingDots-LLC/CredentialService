using cred_system_back_end_app.Domain.Interfaces;
using MediatR;

namespace cred_system_back_end_app.Application.Delegates.Commands
{
    public record class SetDelegateStatusCommand(bool IsActive, int DelegateId, string UserEmail) : IRequest, ITransactionPipeline;
}
