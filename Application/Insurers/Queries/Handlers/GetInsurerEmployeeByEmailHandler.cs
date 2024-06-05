using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using MediatR;

namespace cred_system_back_end_app.Application.Insurers.Queries.Handlers
{
    public class GetInsurerEmployeeByEmailHandler : IRequestHandler<GetInsurerEmployeeByEmailQuery, UserResponseDto?>
    {
        private readonly IInsurerEmployeeRepository _insurerEmployeeRepository;

        public GetInsurerEmployeeByEmailHandler(IInsurerEmployeeRepository insurerEmployeeRepository)
        {
            _insurerEmployeeRepository = insurerEmployeeRepository;
        }

        public async Task<UserResponseDto?> Handle(GetInsurerEmployeeByEmailQuery request, CancellationToken cancellationToken)
        {
            var insurerEmployee = await _insurerEmployeeRepository.GetByInsurerEmployeeEmailAsync(request.InsurerEmployeeEmail);
            if (insurerEmployee == null)
            {
                return null;
            }
            return new UserResponseDto
            {
                Id = insurerEmployee.Id,
                Name = insurerEmployee.Name,
                MiddleName = insurerEmployee.MiddleName,
                LastName = insurerEmployee.LastName,
                Surname = insurerEmployee.SurName,
                Email = insurerEmployee.Email,
                IsActive = insurerEmployee.IsActive,
            };
        }

    }
}
