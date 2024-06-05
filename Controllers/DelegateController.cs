using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.RequestDto;
using cred_system_back_end_app.Application.UseCase.Delegate;
using cred_system_back_end_app.Application.UseCase.Delegate.DTO;
using cred_system_back_end_app.Application.UseCase.Insurer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;

namespace cred_system_back_end_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DelegateController : ControllerBase
    {
        private readonly DelegateCase _delegateCase;
        private readonly ProviderByDelegateCase _providerByDelegateCase;

        public DelegateController(DelegateCase delegateCase, ProviderByDelegateCase providerByDelegateCase)
        {

            _delegateCase = delegateCase;
            _providerByDelegateCase = providerByDelegateCase;

        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("Providers")]
        public IActionResult GetProvidersByDelegate(int delegateId)
        {
            var result = _providerByDelegateCase.GetProviderByDelegateId(delegateId);
            return Ok(result);
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet]
        public IActionResult GetAsync(string email)
        {
            try
            {
                var result = _delegateCase.GetDelegateByEmail(email);
                return Ok(result);
            }
            catch  
            {
                // NOTE: purposefully return 200 with empty body
                return Ok();
            }
            
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("ByProvider")]
        public async Task<IActionResult> Get(int id, int currentPage, int limitPerPage)
        {
            var delegateList = await _delegateCase.GetByProvider(id, currentPage, limitPerPage);

            return Ok(delegateList);
        }
        
        [AllowAnonymous]
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("Exist")]
        public async Task<IActionResult> DoesDelegateExist(string email)
        {
            var doesExist = _delegateCase.DoesDelegateExist(email);

            return Ok(doesExist);
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("CompleteRegistration")]
        public async Task<IActionResult> Create(CreateDelegateDto createDto)
        {
            await _delegateCase.SetEmail(User.FindFirst("emails")?.Value);
            
            var result = await _delegateCase.FinishDelegateRegistrationAsync(createDto);

            return Ok(result);
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("Link")]
        public IActionResult LinkToProvider(CreateProviderDelegateDto createDto)
        {
            _delegateCase.CreateProviderDelegate(createDto);

            return Ok();
        }

        [Authorize(Policy = CredPolicy.ACCESS_AS_PROVIDER)]
        [HttpPut("SetStatus")]
        public async Task<IActionResult> SetDelegateStatus([FromBody] SetDelegateStatusDTO requestDTO)
        {
            _delegateCase.UserEmail = User.FindFirst("emails")?.Value;

            await _delegateCase.SetDelegateStatusAsync(requestDTO.IsActive, requestDTO.DelegateId);

            return Ok();
        }

    }
}
