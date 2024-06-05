using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace cred_system_back_end_app.Infrastructure.B2C
{
    public class B2CTokenMiddleware : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // This method is called after the action executes.
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var email = context.HttpContext.User.FindFirst("Emails")?.Value;
            if (string.IsNullOrEmpty(email))
            {
                throw new KeyNotFoundException(nameof(email));
            }
            context.HttpContext.Items["UserEmail"] = email;

            var role = context.HttpContext.User.FindFirst("extension_Role")?.Value;
            if (string.IsNullOrEmpty(role))
            {
                throw new KeyNotFoundException(nameof(role));
            }

            context.HttpContext.Items["UserRole"] = email;           
        }
    }
}
