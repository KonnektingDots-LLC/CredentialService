﻿using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace cred_system_back_end_app.Application.UseCase.Notifications.NotificationManagers
{
    public class InsurerToProviderStatusRTPNotificationManager : NotificationManagerBase
    {
        private readonly InsurerToProviderStatusRTPCase _statusCase;
        public InsurerToProviderStatusRTPNotificationManager
        (
            InsurerToProviderStatusRTPCase statusCase,
            SaveNotificationCase saveNotificationCase,
            IOptions<SmtpSettings> smtpSettings,
            IConfiguration configuration,
            IWebHostEnvironment env
        ) : base(saveNotificationCase, smtpSettings, configuration, env)
        {
            _statusCase = statusCase;
        }

        public async Task SendNotificationAsync(int providerId, string email, string sentBy)
        {
            SetNotificationData
            (
                providerId,
                NotificationType.INSURER_PROVIDER_STATUS,
                ResourceType.PROVIDER,
                email,
                sentBy
            );

            await SendNotificationAsync
            (
                async () => await _statusCase.SendInsurerToProviderStatusEmailAsync(email)
            );
        }
    }
}
