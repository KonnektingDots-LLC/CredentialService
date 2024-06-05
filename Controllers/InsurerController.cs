using cred_system_back_end_app.Application.UseCase.Insurer;
using cred_system_back_end_app.Application.UseCase.Insurer.DTO.GetProviders;
using cred_system_back_end_app.Application.UseCase.Insurer.DTO.RegisterProvider;
using cred_system_back_end_app.Application.UseCase.Insurer.DTO.ValidateAdmin;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.AspNetCore.Mvc;
using cred_system_back_end_app.Application.UseCase.Insurer.DTO.CreateInsurerEmployee;
using cred_system_back_end_app.Infrastructure.B2C;
using Microsoft.AspNetCore.Authorization;
using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.Mappers.DTOToEntity;
using Microsoft.IdentityModel.Tokens;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Application.Common.RequestDto;
using cred_system_back_end_app.Application.UseCase.Insurer.DTO.UpdateInsurerEmployee;
using cred_system_back_end_app.Application.UseCase.Insurer.DTO.InsurerStatus;
using cred_system_back_end_app.Application.UseCase.ProviderInsurerCompanyStatus;
using cred_system_back_end_app.Application.UseCase.ProviderInsurerCompanyStatus.DTO;

namespace cred_system_back_end_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsurerController : ControllerBase
    {
        private readonly InsurerUseCase _insurerUseCase;
        private readonly GetB2CInfo _getB2CInfo;
        private readonly ProviderInsurerCompanyStatusUseCase _provInsCompStatusUseCase;
        
        public InsurerController(InsurerUseCase insurerUseCase,
            GetB2CInfo getB2CInfo,
            ProviderInsurerCompanyStatusUseCase provInsCompStatusUseCase) 
        { 
            _insurerUseCase = insurerUseCase;
            _getB2CInfo = getB2CInfo;
            _provInsCompStatusUseCase = provInsCompStatusUseCase;
        }

        [AllowAnonymous]
        [HttpPost("ValidateInsurerAdmin")]
        public async Task<IActionResult> ValidateInsurerAdmin([FromBody] ValidateRequestDTO validateAdminRequestDTO)
        {
            var adminExists = await _insurerUseCase.ValidateAdmin(validateAdminRequestDTO.Email);

            var adminValidationResponse = GetValidationResponse(adminExists);

            return Ok(adminValidationResponse);
        }

        [Authorize(Policy = CredPolicy.ACCESS_AS_ADMIN_INSURER)]
        [HttpPut("UpdateInsurerAdmin")]
        public async Task<IActionResult> UpdateInsurerAdmin([FromBody] UpdateInsurerAdminRequestDTO updateInsurerAdminRequestDTO)
        {
            await _insurerUseCase.UpdateInsurerAdmin(
                updateInsurerAdminRequestDTO.Email, 
                updateInsurerAdminRequestDTO.GetNames()                
            );

            return Ok();
        }

        [Authorize(Policy = CredPolicy.ACCESS_AS_ADMIN_INSURER)]
        [HttpGet("GetEmployees")]
        public async Task<IActionResult> GetInsurerEmployees([FromQuery] int currentPage, [FromQuery] int limitPerPage, string? search)
        {
            var insurerAdminEmail = GetEmail();

            var paginatedResponse = new InsurerEmployeePaginatedResponse()
            {
                CurrentPage = currentPage,
                LimitPerPage = limitPerPage
            };

            (IEnumerable<InsurerEmployeeEntity> employees, int employeeCount) employeeTuple;
            if (search.IsNullOrEmpty())
            {
                employeeTuple = await _insurerUseCase.GetEmployees(currentPage, limitPerPage, insurerAdminEmail);
            }             
            else
            {
                employeeTuple = await _insurerUseCase.GetSearchEmployees(currentPage, limitPerPage, insurerAdminEmail,search);
            }
                

            paginatedResponse.TotalNumberOfPages = (int)GetTotalNumberOfPages(limitPerPage, employeeTuple.employeeCount);
            paginatedResponse.Content = GetInsurerPaginatedEmployees(employeeTuple.employees).ToArray();

            return Ok(paginatedResponse);
        }

        [Authorize(Policy = CredPolicy.ACCESS_AS_ADMIN_INSURER)]
        [HttpPut("SetEmployeeStatus")]
        public async Task<IActionResult> SetEmployeeStatus([FromBody] SetEmployeeStatusDTO requestDTO)
        {
            await _insurerUseCase.SetEmployeeStatusAsync(requestDTO.IsActive, requestDTO.Email);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("ValidateInsurer")]
        public async Task<IActionResult> ValidateInsurer(ValidateRequestDTO validateRequestDTO) 
        {
            var employeeExists = await _insurerUseCase.ValidateInsurerEmployee(validateRequestDTO.Email);

            var validationResponse = GetValidationResponse(employeeExists);

            return Ok(validationResponse);
        }

        [Authorize(Policy = CredPolicy.ACCESS_AS_INSURER)]
        [HttpPut("UpdateInsurerEmployee")]
        public async Task<IActionResult> UpdateInsurerEmployee([FromBody] UpdateInsurerEmployeeRequestDto updateInsurerEmployee)
        {
            _getB2CInfo.Email = User.FindFirst(CredTokenKey.EMAIL)?.Value;
            await _insurerUseCase.UpdateInsurerEmployee(updateInsurerEmployee);
            return Ok();
        }

        [Authorize(Policy = CredPolicy.ACCESS_AS_IAINS)]
        [HttpGet("StatusHistory")]
        public async Task<IActionResult> GetStatusSummaryAndHistory(int providerId)
        {
            var role = User.FindFirst(CredTokenKey.ROLE)?.Value;
            _getB2CInfo.Email = User.FindFirst(CredTokenKey.EMAIL)?.Value;
            var response = await _provInsCompStatusUseCase.GetPICSAndPICSHByProviderAndInsurerCompany(providerId, _getB2CInfo.Email, role);
            return Ok(response);
        }

        [Authorize(Policy = CredPolicy.ACCESS_AS_IAINS)]
        [HttpPut("SetStatus")]
        public async Task<IActionResult> SetStatus([FromBody] SetStatusInsurerDto statusInsurerDto)
        {          
            _getB2CInfo.Email = User.FindFirst(CredTokenKey.EMAIL)?.Value;
            await _provInsCompStatusUseCase.SetStatusInsurer(statusInsurerDto, _getB2CInfo.Email);
            return Ok();
        }


        [Authorize(Policy = CredPolicy.ACCESS_AS_PAD)]
        [HttpGet("Status")]
        public async Task<IActionResult> GetInsurersByProvider
        (
            [FromQuery] int currentPage, 
            [FromQuery] int limitPerPage,
            [FromQuery] int providerId
        )
        {
            // TODO: is it a risk to send providerId in query params?

            var paginatedResponse = await _insurerUseCase
                .GetProviderInsurerStatuses(providerId, currentPage, limitPerPage);
            
            return Ok(paginatedResponse);
        }

        #region helpers

        private string GetEmail()
        {
            var insurerAdminEmail = User.FindFirst(CredTokenKey.EMAIL)?.Value;

            if (insurerAdminEmail == null)
            {
                throw new AggregateException("No insurer admin email could be found in token.");
            }

            return insurerAdminEmail;
        }

        private IEnumerable<InsurerEmployeeResponseDTO> GetInsurerPaginatedEmployees(IEnumerable<InsurerEmployeeEntity> employees)
        {
            
            if (employees == null)
            {
                return (IEnumerable<InsurerEmployeeResponseDTO>)(employees = new List<InsurerEmployeeEntity>());
            }
            return employees.Select(employee => new InsurerEmployeeResponseDTO
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

        private static ValidationResponseDTO GetValidationResponse(bool adminExists)
        {
            return new ValidationResponseDTO
            {
                Exists = adminExists,
            };
        }

        private static GetProvidersResponseDTO GetProviderLookupResponse(IEnumerable<ProviderEntity> providers)
        {
            var providerNames = providers
                .Select(p => new ProviderInfo
                {
                    FirstName = p.FirstName,
                    MiddleName = p.MiddleName,
                    Surname = p.SurName,
                    SecondLastName = p.LastName
                })
                .ToArray();

            var response = new GetProvidersResponseDTO()
            {
                Providers = providerNames
            };

            return response;
        }

        private static double GetTotalNumberOfPages(int limit, int providerCount)
        {
            return Math.Ceiling((double)providerCount / limit);
        }

        #endregion
    }
}
