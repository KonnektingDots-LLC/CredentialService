using cred_system_back_end_app.Application.DTO.Responses;
using MediatR;

namespace cred_system_back_end_app.Application.Admin.Queries
{
    public record class GetSpecialtyByOrganizationTypeIdQuery(int OrganizationTypeId) : IRequest<List<SpecialtyResponseDto>>;
}
