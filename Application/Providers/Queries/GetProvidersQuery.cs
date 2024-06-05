using cred_system_back_end_app.Application.DTO;
using cred_system_back_end_app.Application.DTO.Requests;
using cred_system_back_end_app.Application.DTO.Responses;
using MediatR;

namespace cred_system_back_end_app.Application.Providers.Queries
{
    public record class GetProvidersQuery(GetProvidersRequestDto ProvidersRequestDto) : IRequest<PaginatedResponseBaseDto<ProviderBaseDto>>;
}
