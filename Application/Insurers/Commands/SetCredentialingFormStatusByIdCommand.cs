﻿using cred_system_back_end_app.Application.DTO.Requests;
using cred_system_back_end_app.Domain.Interfaces;
using MediatR;

namespace cred_system_back_end_app.Application.Insurers.Commands
{
    public record class SetCredentialingFormStatusByIdCommand(CredentialingFormStatusInsurerRequestDto StatusInsurer, string ModifiedBy) : IRequest, ITransactionPipeline;
}
