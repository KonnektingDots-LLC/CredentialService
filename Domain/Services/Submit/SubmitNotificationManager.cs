using cred_system_back_end_app.Application.Admin.Notifications;
using cred_system_back_end_app.Application.Common.EqualityComparers;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Infrastructure.Data.Repositories;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace cred_system_back_end_app.Domain.Services.Submit
{
    public class SubmitNotificationManager
    {
        private readonly InsurerCompanyRepository _insurerCompanyRepository;
        private readonly DelegateService _delegateRepository;
        private readonly InsurerCompanyEqualityComparer _insurerCompanyEqualityComparer;
        private readonly ProviderInsurerCompanyStatusService _insurerCompanyStatusRepository;
        private readonly IMediator _mediator;

        public SubmitNotificationManager
        (
            InsurerCompanyRepository insurerCompanyRepository,
            ProviderInsurerCompanyStatusService insurerCompanyStatusRepository,
            DelegateService delegateRepository, IMediator mediator
        )
        {
            _insurerCompanyRepository = insurerCompanyRepository;
            _delegateRepository = delegateRepository;

            _insurerCompanyEqualityComparer = new();

            _insurerCompanyStatusRepository = insurerCompanyStatusRepository;
            _mediator = mediator;
        }

        public async Task SendNotificationsAsync(int providerId, string providerEmail, bool isFirstSubmit, List<int> providerInsurerCompanyStatusIds)
        {
            await SendNotificationsToInsurers(providerId, isFirstSubmit, providerInsurerCompanyStatusIds);

            await SendNotificationsToDelegates(providerId);

            await SendNotificationToProvider(providerId, providerEmail);
        }

        #region helpers
        private async Task SendNotificationToProvider(int providerId, string providerEmail)
        {
            await _mediator.Publish(new ProviderSubmitNotification(providerId, providerEmail));
        }

        private async Task SendNotificationsToDelegates(int providerId)
        {
            var providerDelegates = await _delegateRepository.GetProviderDelegatesByProviderId(providerId);

            if (providerDelegates.IsNullOrEmpty()) return;

            foreach (var providerDelegate in providerDelegates)
            {
                if (providerDelegate.IsActive)
                {
                    await _mediator.Publish(new DelegateSubmitNotification(providerId, providerDelegate.Delegate.Email));
                }
            }
        }

        private async Task SendNotificationsToInsurers(int providerId, bool isFirstSubmit, List<int>? providerInsurerCompanyStatusIds)
        {
            var insurerCompanies = await GetInsurerCompanies(providerId, isFirstSubmit, providerInsurerCompanyStatusIds);

            foreach (var insurerCompany in insurerCompanies)
            {
                await _mediator.Publish(new InsurerSubmitNotification(providerId, insurerCompany.NotificationEmail));
            }
        }

        private async Task<IEnumerable<InsurerCompanyEntity>> GetInsurerCompanies(int providerId, bool isFirstSubmit, List<int>? providerInsurerCompanyStatusIds)
        {
            if (!isFirstSubmit)
            {
                if (providerInsurerCompanyStatusIds == null || !providerInsurerCompanyStatusIds.Any())
                { throw new Exception("Not Insurer Company email found."); }

                return (await _insurerCompanyStatusRepository.GetInsurerStatusesByProviderIdAsync(providerId))
                                    .Where(x => providerInsurerCompanyStatusIds.Contains(x.Id))
                                    .Select(x => x.InsurerCompany);
            }

            return (await _insurerCompanyRepository.GetByProvider(providerId))
                .Distinct(_insurerCompanyEqualityComparer);
        }

        #endregion helpers
    }
}
