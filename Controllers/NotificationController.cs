using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.UseCase.Notifications;
using cred_system_back_end_app.Application.UseCase.Notifications.DTO;
using cred_system_back_end_app.Infrastructure.B2C;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using cred_system_back_end_app.Infrastructure.Smtp.ProviderReviewNotification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace cred_system_back_end_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : Controller
    {
        private readonly InviteDelegateCase _inviteDelegateCase;
        private readonly ProviderReviewCase _providerReviewCase;
        private readonly ProfileCompleteCase _profileCompleteCase;
        private readonly InviteInsurerCase _inviteInsurerCase;
        private readonly GetB2CInfo _getB2CInfo;
        private readonly SaveNotificationCase _saveNotificationCase;


        public NotificationController(InviteDelegateCase inviteDelegateCase, 
            ProviderReviewCase providerReviewCase, 
            ProfileCompleteCase profileCompleteCase,
            InviteInsurerCase inviteInsurerCase,
            GetB2CInfo getB2CInfo,
            SaveNotificationCase saveNotificationCase)
        {

            _inviteDelegateCase = inviteDelegateCase;
            _providerReviewCase = providerReviewCase;
            _profileCompleteCase = profileCompleteCase;
            _inviteInsurerCase = inviteInsurerCase;
            _getB2CInfo = getB2CInfo;
            _saveNotificationCase = saveNotificationCase;

        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("Invite")]
        public async Task<IActionResult> SendDelegateInvitationEmail(DelegateInviteInfoDto request)
        {
            await _inviteDelegateCase.SendInvitationEmailAsync(request);

            return Ok();
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("Provider")]
        public async Task<IActionResult> SendProviderReviewEmail(ProviderReviewInfoDto request)
        {
            
            await _providerReviewCase.SendEmailAsync(request);

            return Ok();
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("Complete")]
        public async Task<IActionResult> SendProfileCompleteEmail(ProfileCompleteInfoDto request)
        {

            await _profileCompleteCase.SendEmailAsync(request);

            return Ok();
        }

        [Authorize(Policy = CredPolicy.ACCESS_AS_ADMIN_INSURER)]
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("InsurerEmployee")]
        public async Task<IActionResult> SendInsurerInviteEmail(InsurerInviteInfoDto request)
        {
            _getB2CInfo.Email = User.FindFirst(CredTokenKey.EMAIL)?.Value;
            await _inviteInsurerCase.SendInvitationEmailAsync(request);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("SubmitNotification")]
        public IActionResult SendNotification()
        {
            NotificationStatusEntity notificationStatus = new NotificationStatusEntity
            {
                IsSuccess = true,
                EmailFrom = "test",
                EmailTo = "test",
                Subject = "test",
                Body = "test",
                CreatedBy = "test",
                CreatedDate = DateTime.Now,

            };
            _getB2CInfo.Email = "lleon@wovenware.com";
            var newNotificationStatus = _saveNotificationCase.CreateNotificationStatus(notificationStatus);
            
            NotificationEntity notification = new NotificationEntity
            {
                NotificationTypeId = "PROV",
                ResourceId = "15",
                NotificationStatus = newNotificationStatus,
                CreatedBy = "test",
                CreatedDate = DateTime.Now,

            };

            _saveNotificationCase.CreateNotification(notification);
            _saveNotificationCase.CommitNotification();
            return Ok();
        }

    }
}
