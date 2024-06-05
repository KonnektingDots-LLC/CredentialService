﻿using cred_system_back_end_app.Domain.Entities;
using MediatR;

namespace cred_system_back_end_app.Application.Providers.Notifications
{
    public record class ProviderDelegateInviteNotification(ProviderEntity? Provider, string? DelegateEmail) : INotification;
}