using cred_system_back_end_app.Application.DTO;
using cred_system_back_end_app.Application.DTO.Responses;
using MediatR;

namespace cred_system_back_end_app.Application.Providers.Queries
{
    public record class GetDelegatesByProviderIdQuery(int ProviderId, int CurrentPage, int LimitPerPage) : IRequest<PaginatedResponseBaseDto<DelegateInfoDto>>;
}
