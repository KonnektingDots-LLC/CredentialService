using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Exceptions;
using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using MediatR;

namespace cred_system_back_end_app.Application.Insurers.Queries.Handlers
{
    public class GetInsurerEmployeesHandler : IRequestHandler<GetInsurerEmployeesQuery, PaginatedResponseBaseDto<UserResponseDto>>
    {
        private readonly IInsurerAdminRepository _insurerAdminRepository;
        private readonly IInsurerService _insurerService;

        public GetInsurerEmployeesHandler(IInsurerAdminRepository insurerAdminRepository, IInsurerService insurerService)
        {
            _insurerAdminRepository = insurerAdminRepository;
            _insurerService = insurerService;
        }

        public async Task<PaginatedResponseBaseDto<UserResponseDto>> Handle(GetInsurerEmployeesQuery request, CancellationToken cancellationToken)
        {
            var insurerCompanyId = request.InsurerCompanyId;
            if (insurerCompanyId == null)
            {
                var insurerAdmin = await _insurerAdminRepository.GetByEmailAsync(request.InsurerAdminEmail)
                    ?? throw new InsurerAdminNotFoundException("Insurer admin was not found by email address.");
                insurerCompanyId = insurerAdmin.InsurerCompanyId;
            }
            return await GetInsurerEmployeesAsync(request.CurrentPage, request.LimitPerPage, request.SearchValue, insurerCompanyId);
        }

        private async Task<PaginatedResponseBaseDto<UserResponseDto>> GetInsurerEmployeesAsync(int currentPage, int limitPerPage, string? search, string insurerCompanyId)
        {
            var paginatedResponse = new PaginatedResponseBaseDto<UserResponseDto>()
            {
                CurrentPage = currentPage,
                LimitPerPage = limitPerPage
            };

            var (employees, employeeCount) = await _insurerService.SearchByInsurerCompanyId(currentPage, limitPerPage, insurerCompanyId, search);


            paginatedResponse.TotalNumberOfPages = (int)GetTotalNumberOfPages(limitPerPage, employeeCount);
            paginatedResponse.Content = GetInsurerPaginatedEmployees(employees).ToArray();

            return paginatedResponse;
        }

        private static IEnumerable<UserResponseDto> GetInsurerPaginatedEmployees(IEnumerable<InsurerEmployeeEntity> employees)
        {

            if (employees == null)
            {
                return new List<UserResponseDto>();
            }
            return employees.Select(employee => new UserResponseDto
            {
                Id = employee.Id,
                Name = employee.Name,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                Surname = employee.SurName,
                Email = employee.Email,
                IsActive = employee.IsActive,
            });
        }

        private static double GetTotalNumberOfPages(int limit, int providerCount)
        {
            return Math.Ceiling((double)providerCount / limit);
        }

    }
}
