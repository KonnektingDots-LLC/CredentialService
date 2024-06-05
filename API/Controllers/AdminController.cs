using cred_system_back_end_app.Application.Admin.Commands;
using cred_system_back_end_app.Application.Admin.Notifications;
using cred_system_back_end_app.Application.Admin.Queries;
using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Application.DTO.Requests;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Infrastructure.PdfReport.CredentialingApplication;
using cred_system_back_end_app.Infrastructure.PdfReport.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using System.Reflection;

namespace cred_system_back_end_app.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly NpiHelper _npiHelper;
        private readonly PdfGeneratorClient<IIPCAPdfRootDto> _pdfClient;

        public AdminController(IMediator mediator, NpiHelper npiHelper, PdfGeneratorClient<IIPCAPdfRootDto> pdfClient)
        {
            _mediator = mediator;
            _npiHelper = npiHelper;
            _pdfClient = pdfClient;
        }

        /// <summary>
        /// Check if the OCS admin email is whitelisted.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("users/{email}/valid")]
        public async Task<IActionResult> ValidateOCSAdmin([FromRoute] string email)
        {
            var ocsAdminExists = await _mediator.Send(new GetOcsAdminUserByEmailQuery(email));
            return Ok(new ValidationResponseDto() { Exists = ocsAdminExists != null });
        }

        /// <summary>
        /// Update an OCS admin info.
        /// </summary>
        /// <param name="updateOCSAdminRequestDTO"></param>
        /// <returns></returns>
        [Authorize(Policy = CredPolicy.ACCESS_AS_ADMIN)]
        [HttpPut("users/{email}")]
        public async Task<IActionResult> UpdateOcsAdmin([FromBody] UserRegisterRequestDto updateOCSAdminRequestDTO)
        {
            await _mediator.Send(new UpdateOcsAdminCommand(updateOCSAdminRequestDTO));
            return Ok();
        }

        /// <summary>
        /// I am alive!
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("health")]
        public IActionResult Health()
        {
            string appName = Assembly.GetEntryAssembly()?.GetName().Name;
            return Ok(appName + ":0.0.2");
        }

        /// <summary>
        /// Notify the user that his account was created, and the profile was completed.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="204">Successful request.</response>
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("notifications")]
        public async Task<IActionResult> SendProfileCompleteEmail(NotificationEmailRequestDto request)
        {
            await _mediator.Publish(new ProfileCompletionNotification(request.Email));
            return Ok();
        }

        /// <summary>
        /// Return a list of ui related list of options
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("ui-lists/{name}")]
        public async Task<IActionResult> GetUILists([FromRoute] string name, [FromQuery] int organizationTypeId)
        {
            switch (name)
            {
                case "ProviderType":
                    return Ok(await _mediator.Send(new GetAllProviderTypesQuery()));
                case "AcceptPlanList":
                    return Ok(await _mediator.Send(new GetAllAcceptPlanListQuery()));
                case "AddressState":
                    return Ok(await _mediator.Send(new GetAllAddressStateQuery()));
                case "AddressCountry":
                    return Ok(await _mediator.Send(new GetAllAddressCountryQuery()));
                case "HospitalList":
                    return Ok(await _mediator.Send(new GetAllHospitalListQuery()));
                case "HospitalPrivilegesType":
                    return Ok(await _mediator.Send(new GetAllHospitalPrivilegeListQuery()));
                case "InsuranceProfessionalLiabilityCarrierList":
                    return Ok(await _mediator.Send(new GetAllInsuranceMalpracticeCarrierQuery()));
                case "InsuranceMalpracticeCarrierList":
                    return Ok(await _mediator.Send(new GetAllInsuranceProfessionalLiabilityCarrierQuery()));
                case "Specialty":
                    return Ok(await _mediator.Send(new GetSpecialtyByOrganizationTypeIdQuery(organizationTypeId)));
                case "SubSpecialty":
                    return Ok(await _mediator.Send(new GetSubSpecialtyByOrganizationTypeIdQuery(organizationTypeId)));
                default:
                    return NotFound();
            }
        }

        /// <summary>
        /// A helper API to validate the NPI number.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("helpers/npi/{npi}/valid")]
        public IActionResult IsValidAsync([FromRoute] string npi)
        {
            var response = _npiHelper.IsValidNPI(npi);
            return Ok(response);
        }

        /// <summary>
        /// A helper API to generate a credentialing form based on provided object.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("helpers/credentialing-form")]
        public async Task<FileStreamResult> GeneratePdfAsync(IIPCAPdfRootDto root)
        {

            var result = await _pdfClient.GetPdfAsync(root, "IIPCAPdfHttpTrigger");
            var stream = await result.Content.ReadAsStreamAsync();

            return File(stream, "application/pdf");
        }
    }
}
