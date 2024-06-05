using cred_system_back_end_app.Application.Insurers.Notifications;
using cred_system_back_end_app.Domain.Interfaces;
using MediatR;

namespace cred_system_back_end_app.Application.Insurers.Commands.Handlers
{
    public class CreateInsurerEmployeeHandler : IRequestHandler<CreateInsurerEmployeeCommand>
    {

        private readonly IInsurerService _insurerService;
        private readonly IMediator _mediator;

        public CreateInsurerEmployeeHandler(IInsurerService insurerAdminRepository,
            IMediator mediator)
        {
            _insurerService = insurerAdminRepository;
            _mediator = mediator;
        }

        public async Task Handle(CreateInsurerEmployeeCommand request, CancellationToken cancellationToken)
        {
            var insurerAdmin = await _insurerService.CreateInsurerEmployee(request.InsurerEmployeeEmail, request.UserEmail);

            // send notification
            await _mediator.Publish(new InsurerInvitationNotification(request.UserEmail, insurerAdmin));
        }
    }
}
