using cred_system_back_end_app.Application.Common.Comparers;
using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.CRUD.Delegate;
using cred_system_back_end_app.Application.CRUD.Insurer;
using cred_system_back_end_app.Application.CRUD.ProviderInsurerCompanyStatus;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.IdentityModel.Tokens;

namespace cred_system_back_end_app.Application.UseCase.Notifications.NotificationManagers
{
    public class SubmitNotificationManager
    {
        private readonly InsurerCompanyRepository _insurerCompanyRepository;
        private readonly DelegateRepository _delegateRepository;
        private readonly InsurerCompanyEqualityComparer _insurerCompanyEqualityComparer;
        private readonly InsurerSubmitNotificationManager _insurerSubmitNotificationManager;
        private readonly DelegateSubmitNotificationManager _delegateSubmitNotificationManager;
        private readonly ProviderSubmitNotificationManager _providerSubmitNotificationManager;
        private readonly ProviderInsurerCompanyStatusRepository _insurerCompanyStatusRepository;

        public SubmitNotificationManager
        (
            InsurerSubmitNotificationManager insurerSubmitNotificationManager,
            DelegateSubmitNotificationManager delegateSubmitNotificationManager,
            ProviderSubmitNotificationManager providerSubmitNotificationManager,
            InsurerCompanyRepository insurerCompanyRepository,
            ProviderInsurerCompanyStatusRepository insurerCompanyStatusRepository,
            DelegateRepository delegateRepository
        )
        {
            _insurerCompanyRepository = insurerCompanyRepository;
            _delegateRepository = delegateRepository;
            
            _insurerCompanyEqualityComparer = new();

            _insurerSubmitNotificationManager = insurerSubmitNotificationManager;
            _delegateSubmitNotificationManager = delegateSubmitNotificationManager;
            _providerSubmitNotificationManager = providerSubmitNotificationManager;
            _insurerCompanyStatusRepository = insurerCompanyStatusRepository;
        }

        public async Task SendNotificationsAsync(int providerId, string providerEmail, bool isFirstSubmit,List<int> providerInsurerCompanyStatusIds)
        {
            await SendNotificationsToInsurers(providerId, isFirstSubmit, providerInsurerCompanyStatusIds);

            await SendNotificationsToDelegates(providerId);

            await SendNotificationToProvider(providerId, providerEmail);
        }

        #region helpers
        private async Task SendNotificationToProvider(int providerId, string providerEmail)
        {
            await _providerSubmitNotificationManager.SendNotificationAsync(providerId, providerEmail);
        }

        private async Task SendNotificationsToDelegates(int providerId)
        {
            var providerDelegates = await _delegateRepository.GetProviderDelegatesByProviderId(providerId);

            if (providerDelegates.IsNullOrEmpty()) return;

            foreach (var providerDelegate in providerDelegates)
            {
                if (providerDelegate.IsActive)
                {
                    await _delegateSubmitNotificationManager.SendNotificationAsync(providerId, providerDelegate.Delegate.Email);
                }
            }
        }

        private async Task SendNotificationsToInsurers(int providerId, bool isFirstSubmit, List<int>? providerInsurerCompanyStatusIds)
        {
            var insurerCompanies = await GetInsurerCompanies(providerId, isFirstSubmit, providerInsurerCompanyStatusIds);

            foreach (var insurerCompany in insurerCompanies)
            {
                await _insurerSubmitNotificationManager.SendNotificationAsync(providerId, insurerCompany.NotificationEmail);
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
