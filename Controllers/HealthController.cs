using cred_system_back_end_app.Application.Common.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using System.Net;

namespace cred_system_back_end_app.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {

        public HealthController() { }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet]
        public IActionResult GetAsync()
        {
            return Ok();
        }
    }
}
