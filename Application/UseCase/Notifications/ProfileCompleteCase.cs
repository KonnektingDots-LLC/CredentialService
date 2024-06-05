
using cred_system_back_end_app.Application.CRUD.Notification;
using cred_system_back_end_app.Application.UseCase.Notifications.DTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using cred_system_back_end_app.Infrastructure.Smtp.ProfileCompletionNotification;
using cred_system_back_end_app.Infrastructure.Smtp.ProviderCompletionNotification;

namespace cred_system_back_end_app.Application.UseCase.Notifications
{
    public class ProfileCompleteCase
    {
        private readonly ProfileCompletionNotificationEmail _completionNotificationEmail;
        private readonly NotificationProfileCompletionDetailRepository _profileCompletionRepo;

        public ProfileCompleteCase(
            ProfileCompletionNotificationEmail completionNotificationEmail,
            NotificationProfileCompletionDetailRepository notificationProfielCompletionDetailRepo)
        {
            _completionNotificationEmail = completionNotificationEmail;
            _profileCompletionRepo = notificationProfielCompletionDetailRepo;
        }

        public async Task SendEmailAsync(ProfileCompleteInfoDto request)
        {

            if (await NotificationHasBeenSentAsync(request.Email))
            {
                return;
            }

            ProfileCompletionNotificationRequestDto emailRequest = new ProfileCompletionNotificationRequestDto
            {
                ToEmail = request.Email,
            };

            await _completionNotificationEmail.SendEmailAsync(request.Email);

            await _profileCompletionRepo.Set(
                new NotificationProfileCompletionDetailEntity
                {
                    Email = request.Email,
                    Sent = true,
                });
        }

        private async Task<bool> NotificationHasBeenSentAsync(string email) 
        {
            var completionDetail = await _profileCompletionRepo.GetByEmailAsync(email);

            if (completionDetail == null) 
            {
                return false;
            }

            return completionDetail.Sent;
        } 

    }
}
