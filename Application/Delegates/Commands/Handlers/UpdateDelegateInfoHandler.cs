using cred_system_back_end_app.Application.Admin.Notifications;
using cred_system_back_end_app.Application.DTO;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Interfaces;
using MediatR;

namespace cred_system_back_end_app.Application.Delegates.Commands.Handlers
{
    public class UpdateDelegateInfoHandler : IRequestHandler<UpdateDelegateInfoCommand, CreateDelegateResponseDto>
    {
        private readonly IMediator _mediator;
        private readonly IDelegateService _delegateService;

        public UpdateDelegateInfoHandler(IMediator mediator, IDelegateService delegateService)
        {
            _mediator = mediator;
            _delegateService = delegateService;
        }

        public async Task<CreateDelegateResponseDto> Handle(UpdateDelegateInfoCommand request, CancellationToken cancellationToken)
        {
            return await UpdateDelegateInfoAndNotifyAsync(request.DelegateDto);
        }

        public async Task<CreateDelegateResponseDto> UpdateDelegateInfoAndNotifyAsync(CreateDelegateDto createDelegateDto)
        {
            var delegateEntity = await _delegateService.UpdateDelegateAsync(createDelegateDto.Email, createDelegateDto.FullName);
            await _mediator.Publish(new ProfileCompletionNotification(createDelegateDto.Email));

            return new CreateDelegateResponseDto { Id = delegateEntity.Id };
        }
    }
}
