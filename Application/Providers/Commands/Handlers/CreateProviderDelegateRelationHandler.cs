using cred_system_back_end_app.Application.Providers.Notifications;
using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using MediatR;

namespace cred_system_back_end_app.Application.Providers.Commands.Handlers
{
    public class CreateProviderDelegateRelationHandler : IRequestHandler<CreateProviderDelegateRelationCommand>
    {
        private readonly IDelegateService _delegateService;
        private readonly IMediator _mediator;

        public CreateProviderDelegateRelationHandler(DbContextEntity contextEntity,
            IDelegateService delegateService,
             IMediator mediator)
        {
            _delegateService = delegateService;
            _mediator = mediator;
        }

        public async Task Handle(CreateProviderDelegateRelationCommand request, CancellationToken cancellationToken)
        {
            if (request.SendInvite)
            {
                var provider = await _delegateService.CreateProviderDelegateRelationAsync(request.ProviderId, request.DelegateEmail);
                await _mediator.Publish(new ProviderDelegateInviteNotification(provider, request.DelegateEmail));
            }
            else
            {
                await _delegateService.CreateProviderDelegateRelationAsync(request.ProviderId, request.DelegateId);
            }
        }


    }
}
