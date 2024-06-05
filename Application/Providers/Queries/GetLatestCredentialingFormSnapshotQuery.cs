using cred_system_back_end_app.Application.DTO.Responses;
using MediatR;

namespace cred_system_back_end_app.Application.Providers.Queries
{
    public record class GetLatestCredentialingFormSnapshotQuery(int ProviderId) : IRequest<CredentialingFormSnapshotResponseDto>;
}
