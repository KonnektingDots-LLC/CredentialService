using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using MediatR;

namespace cred_system_back_end_app.Application.Insurers.Queries.Handlers
{
    public class GetInsurerAdminByEmailHandler : IRequestHandler<GetInsurerAdminByEmailQuery, UserResponseDto?>
    {
        private readonly IInsurerAdminRepository _insurerAdminRepository;

        public GetInsurerAdminByEmailHandler(IInsurerAdminRepository insurerAdminRepository)
        {
            _insurerAdminRepository = insurerAdminRepository;
        }

        public async Task<UserResponseDto?> Handle(GetInsurerAdminByEmailQuery request, CancellationToken cancellationToken)
        {
            var insurerAdmin = await _insurerAdminRepository.GetByEmailAsync(request.InsurerAdminEmail);
            if (insurerAdmin == null)
            {
                return null;
            }
            return new UserResponseDto
            {
                Id = insurerAdmin.Id,
                Name = insurerAdmin.Name,
                MiddleName = insurerAdmin.MiddleName,
                LastName = insurerAdmin.LastName,
                Surname = insurerAdmin.Surname,
                Email = insurerAdmin.Email,
                IsActive = insurerAdmin.IsActive,
            };
        }

    }
}
