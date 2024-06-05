using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Delegates.Commands;
using cred_system_back_end_app.Application.Delegates.Notifications;
using cred_system_back_end_app.Application.Delegates.Queries;
using cred_system_back_end_app.Application.DTO;
using cred_system_back_end_app.Application.DTO.Requests;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Application.Providers.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace cred_system_back_end_app.API.Controllers
{
    [Route("api/delegates")]
    [ApiController]
    [AllowAnonymous]
    public class DelegatesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DelegatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Search for delegate by email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <response code="200">Successful request.</response>
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string email)
        {
            var delegateFound = await _mediator.Send(new GetDelegateByEmailQuery(email));
            return Ok(delegateFound);
        }

        /// <summary>
        /// Check if the insurer employee email is whitelisted.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <response code="200">Successful request.</response>
        [AllowAnonymous]
        [HttpGet("{email}/valid")]
        public async Task<IActionResult> ValidateDelegate([FromRoute] string email)
        {
            DelegateResponseDto? delegateFound = null;
            try
            {
                delegateFound = await _mediator.Send(new GetDelegateByEmailQuery(email));
            }
            catch (System.Exception)
            {
                //Do nothing
            }
            return Ok(new ValidationResponseDto() { Exists = delegateFound != null });
        }

        /// <summary>
        /// Update delegate details and notify.
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        /// <response code="200">Successful request.</response>
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPut]
        public async Task<IActionResult> UpdateDelegate(CreateDelegateDto createDto)
        {
            var result = await _mediator.Send(new UpdateDelegateInfoCommand(createDto));
            return Ok(result);
        }

        /// <summary>
        /// Create provider-delegate relation. The delegate and provider must exist.
        /// </summary>
        /// <param name="id">The delegate id</param>
        /// <param name="providerId">The provider id</param>
        /// <returns></returns>
        /// <response code="200">Successful request.</response>
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("{id}/providers/{providerId}")]
        public async Task<IActionResult> LinkToProvider([FromRoute] int id, [FromRoute] int providerId)
        {
            await _mediator.Send(new CreateProviderDelegateRelationCommand(ProviderId: providerId, DelegateId: id, SendInvite: false));
            return Ok();
        }

        /// <summary>
        /// Update delegate status.
        /// </summary>
        /// <param name="id">The delegate id</param>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        /// <response code="204">Successful request.</response>
        [Authorize(Policy = CredPolicy.ACCESS_AS_PROVIDER)]
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SetDelegateStatus([FromRoute] int id, [FromBody] StatusRequestDto requestDTO)
        {
            await _mediator.Send(new SetDelegateStatusCommand(requestDTO.IsActive, id, User.FindFirst("emails")?.Value));
            return Ok();
        }

        /// <summary>
        /// Retrieve a list of providers for a delegate.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Successful request.</response>
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("{id}/providers")]
        public async Task<IActionResult> GetProvidersByDelegate([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetProvidersByDelegateIdQuery(id));
            return Ok(result);
        }

        /// <summary>
        /// Notify the provider that the credentialing form is completed.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Successful request.</response>
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("{id}/providers/{providerId}/notification")]
        public async Task<IActionResult> SendProviderReviewEmail(NotificationEmailRequestDto request)
        {
            //TODO:: validate delegate is associated to provider
            await _mediator.Publish(new ProviderReviewNotification(request.Email));
            return Ok();
        }
    }
}
