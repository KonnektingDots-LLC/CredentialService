using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Identity.Web;
using System.Security.Claims;

namespace cred_system_back_end_app.Infrastructure.B2C
{
    public class MyCustomAuthAttribute : AuthorizeAttribute
    {
        public string Email{ get; set; }
        public MyCustomAuthAttribute(AuthorizationHandlerContext context)
        {

            Email = context?.User?.FindFirst("emails").Value;
        }
    }
}
