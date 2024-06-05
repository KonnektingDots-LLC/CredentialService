using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.UseCase.Submit;
using cred_system_back_end_app.Infrastructure.B2C;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace cred_system_back_end_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmitController : ControllerBase
    {
        private SubmitCase _submitCase;

        public SubmitController(SubmitCase submitCase)
        {
            _submitCase = submitCase;
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("SubmitAll")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> SubmitAll(SubmitRequestDTO submitData) 
        {
            var email = User.FindFirst(CredTokenKey.EMAIL)?.Value;

            var pdfDocumentResponse = await _submitCase.SubmitAll(submitData, email);

            return Ok(pdfDocumentResponse);
        }
    }
}
