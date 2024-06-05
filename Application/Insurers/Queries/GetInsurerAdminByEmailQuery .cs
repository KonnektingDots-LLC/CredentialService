using cred_system_back_end_app.Application.DTO.Responses;
using MediatR;

namespace cred_system_back_end_app.Application.Insurers.Queries
{
    public record class GetInsurerAdminByEmailQuery(string? InsurerAdminEmail) : IRequest<UserResponseDto>;
}
