using cred_system_back_end_app.Domain.Interfaces;
using MediatR;

namespace cred_system_back_end_app.Application.Insurers.Commands.Handlers
{
    public class SetInsurerEmployeeStatusHandler : IRequestHandler<SetInsurerEmployeeStatusCommand>
    {
        private readonly IInsurerService _insurerService;

        public SetInsurerEmployeeStatusHandler(IInsurerService insurerService)
        {
            _insurerService = insurerService;
        }

        public async Task Handle(SetInsurerEmployeeStatusCommand request, CancellationToken cancellationToken)
        {
            await _insurerService.SetInsurerEmployeeStatusByEmailAsync(request.IsActive, request.InsurerEmployeeEmail);
        }
    }
}
