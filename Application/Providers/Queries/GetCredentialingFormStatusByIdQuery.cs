using cred_system_back_end_app.Application.DTO.Responses;
using MediatR;

namespace cred_system_back_end_app.Application.Providers.Queries
{
    public record class GetCredentialingFormStatusByIdQuery(int credentialingFormId) : IRequest<ProviderCredFormResponseDto>;
}
