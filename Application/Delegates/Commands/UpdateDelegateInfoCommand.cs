using cred_system_back_end_app.Application.DTO;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Interfaces;
using MediatR;

namespace cred_system_back_end_app.Application.Delegates.Commands
{
    public record class UpdateDelegateInfoCommand(CreateDelegateDto DelegateDto) : IRequest<CreateDelegateResponseDto>, ITransactionPipeline;
}
