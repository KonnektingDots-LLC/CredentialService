using cred_system_back_end_app.Application.Delegates.Notifications;
using cred_system_back_end_app.Application.Providers.Notifications;
using cred_system_back_end_app.Domain.Interfaces;
using MediatR;

namespace cred_system_back_end_app.Application.Delegates.Commands.Handlers
{
    public class SetDelegateStatusHandler : IRequestHandler<SetDelegateStatusCommand>
    {
        private readonly IDelegateService _delegateService;
        private readonly IMediator _mediator;

        public SetDelegateStatusHandler(IDelegateService delegateService, IMediator mediator)
        {
            _delegateService = delegateService;
            _mediator = mediator;
        }

        public async Task Handle(SetDelegateStatusCommand request, CancellationToken cancellationToken)
        {
            var (providerEntity, delegateEntity) = await _delegateService.UpdateProviderDelegateStatusAsync(request.IsActive, request.DelegateId, request.UserEmail);

            await _mediator.Publish(new DelegateStatusUpdateNotification(providerEntity, delegateEntity));

            await _mediator.Publish(new ProviderDelegateStatusNotification(providerEntity, delegateEntity));
        }
    }
}
