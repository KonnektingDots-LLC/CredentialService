using cred_system_back_end_app.Application.DTO.Requests;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Interfaces;
using MediatR;

namespace cred_system_back_end_app.Application.Providers.Commands
{
    public record class CreateProviderCommand(CreateProviderRequestDto CreateProviderRequestDto) : IRequest<CreateCredFormProviderResponseDto>, ITransactionPipeline;
}
