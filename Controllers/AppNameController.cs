using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using System.Reflection;

namespace cred_system_back_end_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppNameController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAsync()
        {
            string appName = Assembly.GetEntryAssembly()?.GetName().Name;
            return Ok(appName+":0.0.2");
        }
    }
}
