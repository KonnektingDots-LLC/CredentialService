using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.CRUD.Provider.DTO;
using cred_system_back_end_app.Application.UseCase.CredForm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace cred_system_back_end_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CredFormController : ControllerBase
    {
        private readonly CredFormUseCase _credFormUseCase;

        public CredFormController(CredFormUseCase credFormUseCase)
        {
            _credFormUseCase = credFormUseCase;
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("ByEmail")]
        public async Task<IActionResult> GetProviderByEmail(string email)
        {
            var credForm = await _credFormUseCase.GetCredFormByEmail(email);
            return Ok(credForm);
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateProviderDto createProviderDto)
        {
            var credFormProvider = await _credFormUseCase.CreateCredFormVersion(User.FindFirst("emails")?.Value, createProviderDto);
            return Ok(credFormProvider);
        }
    }
}
