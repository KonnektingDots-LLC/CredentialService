using cred_system_back_end_app.Application.DTO.Responses;
using MediatR;

namespace cred_system_back_end_app.Application.Delegates.Queries
{
    public record class GetProvidersByDelegateIdQuery(int DelegateId) : IRequest<List<ProviderByDelegateResponseDto>>;
}
