using cred_system_back_end_app.Domain.Interfaces;
using MediatR;

namespace cred_system_back_end_app.Application.Providers.Commands
{
    public record class CreateProviderDelegateRelationCommand(int ProviderId, string? DelegateEmail = null, int DelegateId = 0, bool SendInvite = false) : IRequest, ITransactionPipeline;
}
