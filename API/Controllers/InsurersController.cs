using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.DTO.Requests;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Application.Insurers.Commands;
using cred_system_back_end_app.Application.Insurers.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace cred_system_back_end_app.API.Controllers
{
    [Route("api/insurers")]
    [ApiController]
    public class InsurersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InsurersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Return credentialing form status history for a provider and insurer company.
        /// </summary>
        /// <param name="providerId">The provider id</param>
        /// <returns></returns>
        /// <response code="200">Successful request.</response>
        [Authorize(Policy = CredPolicy.ACCESS_AS_IAINS)]
        [HttpGet("providers/{providerId}/credentialing-forms/statuses")]
        public async Task<IActionResult> GetStatusSummaryAndHistory([FromRoute] int providerId)
        {
            var role = User.FindFirst(CredTokenKey.ROLE)?.Value;
            var email = User.FindFirst(CredTokenKey.EMAIL)?.Value;
            var response = await _mediator.Send(new GetCredentialingFormStatusByProviderIdAndInsurerQuery(providerId, email, role));
            return Ok(response);
        }

        /// <summary>
        /// Update a credentialing form status.
        /// </summary>
        /// <param name="statusInsurerDto"></param>
        /// <returns></returns>
        [Authorize(Policy = CredPolicy.ACCESS_AS_IAINS)]
        [HttpPatch("providers/{providerId}/credentialing-forms/statuses")]
        public async Task<IActionResult> SetStatus([FromBody] CredentialingFormStatusInsurerRequestDto statusInsurerDto)
        {
            var email = User.FindFirst(CredTokenKey.EMAIL)?.Value;
            await _mediator.Send(new SetCredentialingFormStatusByIdCommand(statusInsurerDto, email));
            return Ok();
        }


        #region insurer employees api definitions

        /// <summary>
        /// Retrieve a list of insurer employees.
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="limitPerPage"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        /// <response code="200">Successful request.</response>
        [Authorize(Policy = CredPolicy.ACCESS_AS_ADMIN_INSURER)]
        [HttpGet("employees")]
        public async Task<IActionResult> GetInsurerEmployees([FromQuery] int currentPage, [FromQuery] int limitPerPage, [FromQuery] string? search)
        {
            var insurerAdminEmail = User.FindFirst(CredTokenKey.EMAIL)?.Value;
            var paginatedResponse = await _mediator.Send(new GetInsurerEmployeesQuery(currentPage, limitPerPage, insurerAdminEmail, search));
            return Ok(paginatedResponse);
        }

        /// <summary>
        /// Create and invite a new insurer employee.;
        /// </summary>
        /// <param name="id">Insurer admin id</param>
        /// <param name="emailDto"></param>
        /// <returns></returns>
        /// <response code="200">Successful request.</response>
        [Authorize(Policy = CredPolicy.ACCESS_AS_ADMIN_INSURER)]
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("employees")]
        public async Task<IActionResult> CreateInsurerEmployee([FromBody] NotificationEmailRequestDto emailDto)
        {
            await _mediator.Send(new CreateInsurerEmployeeCommand(emailDto.Email, User.FindFirst(CredTokenKey.EMAIL)?.Value));
            return Ok();
        }

        /// <summary>
        /// Check if the insurer employee email is whitelisted.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <response code="200">Successful request.</response>
        [AllowAnonymous]
        [HttpPost("employees/{email}/valid")]
        public async Task<IActionResult> ValidateInsurer([FromRoute] string email)
        {
            var employeeExists = await _mediator.Send(new GetInsurerEmployeeByEmailQuery(email));
            return Ok(new ValidationResponseDto() { Exists = employeeExists != null });
        }

        /// <summary>
        /// Set if the insurer employee is active or not.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        /// <response code="200">Successful request.</response>
        [Authorize(Policy = CredPolicy.ACCESS_AS_ADMIN_INSURER)]
        [HttpPatch("employees/{email}")]
        public async Task<IActionResult> SetEmployeeStatus([FromRoute] string email, [FromBody] StatusRequestDto requestDTO)
        {
            await _mediator.Send(new SetInsurerEmployeeStatusCommand(requestDTO.IsActive, email));
            return Ok();
        }

        /// <summary>
        /// Update an insurer employee.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="updateInsurerEmployee"></param>
        /// <returns></returns>
        [Authorize(Policy = CredPolicy.ACCESS_AS_INSURER)]
        [HttpPut("employees/{email}")]
        public async Task<IActionResult> UpdateInsurerEmployee([FromRoute] string email, [FromBody] UserRegisterRequestDto updateInsurerEmployee)
        {
            updateInsurerEmployee.Email = email;
            await _mediator.Send(new UpdateInsurerEmployeeCommand(updateInsurerEmployee));
            return Ok();
        }

        #endregion

        #region insurer admins api definitions

        /// <summary>
        /// Update an insurer admin.
        /// </summary>
        /// <param name="updateInsurerAdminRequestDTO"></param>
        /// <returns></returns>
        /// <response code="200">Successful request.</response>
        /// <response code="404">The insurer admin was not found.</response>
        [Authorize(Policy = CredPolicy.ACCESS_AS_ADMIN_INSURER)]
        [HttpPut("admins")]
        public async Task<IActionResult> UpdateInsurerAdmin([FromBody] UserRegisterRequestDto updateInsurerAdminRequestDTO)
        {
            await _mediator.Send(new UpdateInsurerAdminCommand(updateInsurerAdminRequestDTO));
            return Ok();
        }

        /// <summary>
        /// Check if the insurer admin email is whitelisted.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <response code="200">Successful request.</response>
        [AllowAnonymous]
        [HttpPost("admins/{email}/valid")]
        public async Task<IActionResult> ValidateInsurerAdmin([FromRoute] string email)
        {
            var adminExists = await _mediator.Send(new GetInsurerAdminByEmailQuery(email));
            return Ok(new ValidationResponseDto() { Exists = adminExists != null });
        }

        #endregion
    }
}
