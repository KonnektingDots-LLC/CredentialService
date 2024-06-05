using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.CRUD.NPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using System.Security.Claims;

namespace cred_system_back_end_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NPIController : ControllerBase
    {

        private readonly NPICase _nPICase;
        //private AuthorizationHandlerContext _auth;

        public NPIController(NPICase nPICase)
        {
            _nPICase = nPICase;
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("IsValid")]
        public IActionResult IsValidAsync(string request)
        {           
            var response = _nPICase.IsValidNPI(request);
            return Ok(response);
        }
    }
}
