using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using MediatR;

namespace cred_system_back_end_app.Application.Admin.Queries.Handlers
{
    public class GetOcsAdminUserByEmailHandler : IRequestHandler<GetOcsAdminUserByEmailQuery, UserResponseDto?>
    {
        private readonly IOcsAdminRepository _ocsAdminRepository;

        public GetOcsAdminUserByEmailHandler(IOcsAdminRepository ocsAdminRepository)
        {
            _ocsAdminRepository = ocsAdminRepository;
        }

        public async Task<UserResponseDto?> Handle(GetOcsAdminUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var adminEntity = await _ocsAdminRepository.GetByEmailAsync(request.Email);
            if (adminEntity == null)
            {
                return null;
            }
            return new UserResponseDto
            {
                Id = adminEntity.Id,
                Email = adminEntity.Email,
                Name = adminEntity.Name,
                MiddleName = adminEntity.MiddleName,
                LastName = adminEntity.LastName,
                IsActive = adminEntity.IsActive,
            };
        }
    }
}
