using AutoMapper;
using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.DTO;
using cred_system_back_end_app.Application.DTO.Requests;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Application.Insurers.Notifications;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using MediatR;

namespace cred_system_back_end_app.Application.Insurers.Commands.Handlers
{
    public class SetCredentialingFormStatusByIdHandler : IRequestHandler<SetCredentialingFormStatusByIdCommand>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IProviderInsurerCompanyStatusService _providerInsurerCompanyStatusService;
        private readonly IProviderInsurerCompanyStatusHistoryRepository _providerInsurerCompanyStatusHistoryRepo;
        private readonly ICredFormRepository _credFormRepo;
        private readonly IProviderService _providerService;
        private readonly IDelegateService _delegateService;

        public SetCredentialingFormStatusByIdHandler(IMediator mediator, IMapper mapper, IProviderInsurerCompanyStatusService providerInsurerCompanyStatusService,
            IProviderService providerService, IProviderInsurerCompanyStatusHistoryRepository providerInsurerCompanyStatusHistoryRepository,
            IDelegateService delegateService, ICredFormRepository credFormRepository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _providerInsurerCompanyStatusService = providerInsurerCompanyStatusService;
            _providerService = providerService;
            _providerInsurerCompanyStatusHistoryRepo = providerInsurerCompanyStatusHistoryRepository;
            _delegateService = delegateService;
            _credFormRepo = credFormRepository;
        }

        public async Task Handle(SetCredentialingFormStatusByIdCommand request, CancellationToken cancellationToken)
        {
            await SetStatusInsurer(request.StatusInsurer, request.ModifiedBy);
        }

        public async Task SetStatusInsurer(CredentialingFormStatusInsurerRequestDto statusInsurer, string modifiedBy)
        {
            var dateNow = DateTime.Now;
            var picsToUpdate = await _providerInsurerCompanyStatusService.GetProviderInsurerCompanyStatusByIdAsync(statusInsurer.Id);
            var providerId = picsToUpdate.ProviderId;

            //Validations
            if (statusInsurer.StatusCode == StatusType.PENDING)
            {
                throw new RequestInvalidException();
            }
            if ((statusInsurer.StatusCode == StatusType.RETURNED_TO_PROVIDER || statusInsurer.StatusCode == StatusType.APPROVED)
                && picsToUpdate.InsurerStatusTypeId != StatusType.PENDING)
            {
                throw new RequestInvalidException();
            }
            if (statusInsurer.StatusCode == StatusType.REJECTED
                    && picsToUpdate.InsurerStatusTypeId != StatusType.RETURNED_TO_PROVIDER)
            {
                throw new RequestInvalidException();
            }
            if (statusInsurer.Comment == null)
            {
                throw new RequestInvalidException();
            }

            picsToUpdate.InsurerStatusTypeId = statusInsurer.StatusCode;
            picsToUpdate.CurrentStatusDate = dateNow;
            picsToUpdate.SubmitDate = dateNow;
            picsToUpdate.Comment = statusInsurer.Comment;
            picsToUpdate.CommentDate = dateNow;
            picsToUpdate.ModifiedBy = modifiedBy;

            var picsUpdated = await _providerInsurerCompanyStatusService.UpdateProviderInsurerCompanyStatus(picsToUpdate);
            ProviderInsurerCompanyStatusHistoryDto picsh = new ProviderInsurerCompanyStatusHistoryDto
            {
                ProviderInsurerCompanyStatusId = picsUpdated.Id,
                InsurerStatusTypeId = picsUpdated.InsurerStatusTypeId,
                StatusDate = picsUpdated.CurrentStatusDate,
                Comment = picsUpdated.Comment,
                CommentDate = picsUpdated.CommentDate,
                CreatedBy = picsUpdated.ModifiedBy
            };
            await CreateProviderInsurerCompanyStatusHistory(picsh);

            await SetCredFormStatusByPICS(providerId, modifiedBy);

            var providerEmail = (await _providerService.GetProviderById(providerId)).Email;

            if (picsUpdated.InsurerStatusTypeId == StatusType.APPROVED || picsUpdated.InsurerStatusTypeId == StatusType.REJECTED)
            {
                await SendNotificationToProviderStatus(picsUpdated.ProviderId, providerEmail, modifiedBy);
                await SendNotificationsToDelegates(providerId, modifiedBy, false);
            }
            else if (picsUpdated.InsurerStatusTypeId == StatusType.RETURNED_TO_PROVIDER)
            {
                await SendNotificationToProviderStatusRTP(picsUpdated.ProviderId, providerEmail, modifiedBy);
                await SendNotificationsToDelegates(providerId, modifiedBy, true);
            }

        }

        private async Task<ProviderInsurerCompanyStatusHistoryResponseDto>
            CreateProviderInsurerCompanyStatusHistory(ProviderInsurerCompanyStatusHistoryDto ProviderInsurerCompanyStatusHistory)
        {
            var newProviderInsurerCompanyStatusHistory = _mapper.Map<ProviderInsurerCompanyStatusHistoryEntity>(ProviderInsurerCompanyStatusHistory);
            newProviderInsurerCompanyStatusHistory = await _providerInsurerCompanyStatusHistoryRepo.AddAndSaveAsync(newProviderInsurerCompanyStatusHistory);
            var newProviderInsurerCompanyStatusHistoryResponse = _mapper.Map<ProviderInsurerCompanyStatusHistoryResponseDto>(newProviderInsurerCompanyStatusHistory);
            return newProviderInsurerCompanyStatusHistoryResponse;
        }

        private async Task SetCredFormStatusByPICS(int providerId, string modifiedBy)
        {
            var credFormId = (await _providerService.GetProviderById(providerId)).CredFormId;
            var pics = await _providerInsurerCompanyStatusService.GetInsurerStatusesByProviderIdAsync(providerId);
            var picsRTPResult = pics.Where(r => r.InsurerStatusTypeId == StatusType.RETURNED_TO_PROVIDER).ToList();
            var picsCloseResult = pics.Where(r => r.InsurerStatusTypeId == StatusType.PENDING || r.InsurerStatusTypeId == StatusType.RETURNED_TO_PROVIDER).ToList();
            var credFormDto = new SetCredFormStatusDto();
            credFormDto.Id = credFormId;
            credFormDto.ModifiedBy = modifiedBy;

            if (picsRTPResult.Count > 0)
            {
                credFormDto.Status = StatusType.RETURNED_TO_PROVIDER;
                await _credFormRepo.SetStatusAndSave(credFormDto.Id, credFormDto.Status);
                return;
            }


            if (picsCloseResult.Count == 0)
            {
                var picsFoundAprove = pics.Where(r => r.InsurerStatusTypeId == StatusType.APPROVED).ToList();
                if (picsFoundAprove.Count > 0) { credFormDto.Status = StatusType.APPROVED; }
                else { credFormDto.Status = StatusType.REJECTED; }

                await _credFormRepo.SetStatusAndSave(credFormDto.Id, credFormDto.Status);
                return;
            }
        }

        #region helpers

        private async Task SendNotificationToProviderStatus(int providerId, string providerEmail, string sentBy)
        {
            await _mediator.Publish(new InsurerToProviderStatusNotification(providerId, providerEmail, sentBy));
        }

        private async Task SendNotificationToProviderStatusRTP(int providerId, string providerEmail, string sentBy)
        {
            await _mediator.Publish(new InsurerToProviderStatusRtpNotification(providerId, providerEmail, sentBy));
        }

        private async Task SendNotificationsToDelegates(int providerId, string sentBy, bool isRTP)
        {
            var providerDelegates = await _delegateService.GetProviderDelegatesByProviderId(providerId);

            if (providerDelegates == null) return;

            foreach (var providerDelegate in providerDelegates)
            {
                if (providerDelegate.IsActive)
                {
                    if (isRTP)
                    {
                        await SendNotificationToProviderStatusRTP(providerId, providerDelegate.Delegate.Email, sentBy);
                    }
                    else
                    {
                        await SendNotificationToProviderStatus(providerId, providerDelegate.Delegate.Email, sentBy);
                    }

                }
            }
        }


        #endregion
    }
}
