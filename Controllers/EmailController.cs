using Microsoft.AspNetCore.Mvc;
using cred_system_back_end_app.Infrastructure.Smtp.DelegateInvitationNotification;
using cred_system_back_end_app.Infrastructure.Smtp.ProviderReviewNotification;
using cred_system_back_end_app.Infrastructure.Smtp.ProfileCompletionNotification;
using cred_system_back_end_app.Infrastructure.Smtp.ProviderCompletionNotification;
using Microsoft.Identity.Web;
using cred_system_back_end_app.Application.Common.Constants;

namespace cred_system_back_end_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly DelegateInvitationNotificationEmail _delegateNotificationEmail;
        private readonly ProviderReviewNotificationEmail _providerNotificationEmail;
        private readonly ProfileCompletionNotificationEmail _completionNotificationEmail;

        public EmailController(DelegateInvitationNotificationEmail delegateNotificationEmail, ProviderReviewNotificationEmail providerNotificationEmail, ProfileCompletionNotificationEmail completionNotificationEmail)
        {
            _delegateNotificationEmail = delegateNotificationEmail;
            _providerNotificationEmail = providerNotificationEmail;
            _completionNotificationEmail = completionNotificationEmail;
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("delegate")]
        public async Task<IActionResult> SendDelegateInvitationEmail(string to)
        {
            DelegateInvitationNotificationRequestDto emailRequest = new DelegateInvitationNotificationRequestDto
            {
                ToEmail = to,
                ProviderName = "John Doe",
                Link = "Microsoft.com"
            };

            await _delegateNotificationEmail.SendEmailAsync(emailRequest);

            return Ok();
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("provider")]
        public async Task<IActionResult> SendProviderReviewEmail(string to)
        {
            ProviderReviewNotificationRequestDto emailRequest = new ProviderReviewNotificationRequestDto
            {
                ToEmail = to,
                Link = "Microsoft.com"
            };

            await _providerNotificationEmail.SendEmailAsync(emailRequest);

            return Ok();
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("completed")]
        public async Task<IActionResult> SendProfileCompletionEmail(string to)
        {
            ProfileCompletionNotificationRequestDto emailRequest = new ProfileCompletionNotificationRequestDto
            {
                ToEmail = to,
            };

            await _completionNotificationEmail.SendEmailAsync(to);

            return Ok();
        }
    }
}
