using cred_system_back_end_app.Application.DTO.Responses;
using MediatR;

namespace cred_system_back_end_app.Application.Insurers.Queries
{
    public record class GetInsurerEmployeeByEmailQuery(string? InsurerEmployeeEmail) : IRequest<UserResponseDto?>;
}
