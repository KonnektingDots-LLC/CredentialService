using AutoMapper;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Application.CRUD.Delegate;
using cred_system_back_end_app.Application.UseCase.Notifications.DTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using cred_system_back_end_app.Infrastructure.Smtp.DelegateInvitationNotification;
using Microsoft.EntityFrameworkCore.Storage;
using Org.BouncyCastle.Crypto;

namespace cred_system_back_end_app.Application.UseCase.Notifications
{
    public class InviteDelegateCase
    {
        private readonly DbContextEntity _contextEntity;
        private readonly DelegateInvitationNotificationEmail _delegateNotificationEmail;
        private readonly IConfiguration _configuration;
        private readonly DelegateRepository _delegateRepo;

        public InviteDelegateCase(DbContextEntity contextEntity, 
            DelegateInvitationNotificationEmail delegateNotificationEmail,
            DelegateRepository delegateRepository,
            IConfiguration configuration)
        {
            _contextEntity = contextEntity;
            _delegateNotificationEmail = delegateNotificationEmail;
            _configuration = configuration;
            _delegateRepo = delegateRepository;
        }

        public async Task SendInvitationEmailAsync(DelegateInviteInfoDto request)
        {
            var provider = _contextEntity.Provider.FirstOrDefault(p => p.Id == request.ProviderId);

            if (provider == null)
            {
                throw new EntityNotFoundException();
            }

            var delegateEntity = (await _delegateRepo.GetDelegatesByEmailAsync(request.Email)).FirstOrDefault();

            await LinkWithProvider(delegateEntity, request, provider.Email);

            DelegateInvitationNotificationRequestDto emailRequest = new DelegateInvitationNotificationRequestDto
            {
                ToEmail = request.Email,
                ProviderName = provider.FirstName + " " + provider.LastName,
                Link = _configuration["FeUrl"] + "?event=DIBP&providerId=" + provider.Id + "&email=" + request.Email,
                ProviderId = provider.Id
            };

            await _delegateNotificationEmail.SendEmailAsync(emailRequest);
        }

        private async Task LinkWithProvider(DelegateEntity delegateEntity, DelegateInviteInfoDto request, string providerEmail)
        {
            ProviderDelegateEntity providerDelegate;

            if (delegateEntity != null) 
            {
                providerDelegate = new ProviderDelegateEntity
                {
                    DelegateId = delegateEntity.Id,
                    ProviderId = request.ProviderId,
                    CreatedBy = providerEmail,
                    CreationDate = DateTime.Now,
                };

                await _delegateRepo.InsertIfNotExistsAsync(providerDelegate);

                return;
            }

            providerDelegate = new ProviderDelegateEntity
            {
                ProviderId = request.ProviderId,
                CreatedBy = providerEmail,
                CreationDate = DateTime.Now,
                Delegate = new DelegateEntity
                {
                    Email = request.Email,
                    CreatedBy = providerEmail,
                    DelegateTypeId = 2,
                    DelegateCompanyId = 2,
                    CreationDate = DateTime.Now,
                    FullName = "",
                    IsActive = false,
                }
            };

            await _delegateRepo.InsertAsync(providerDelegate);
        }
    }
}
