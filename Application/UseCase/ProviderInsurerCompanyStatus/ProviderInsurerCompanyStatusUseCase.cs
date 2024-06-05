using AutoMapper;
using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Application.CRUD.CredForm;
using cred_system_back_end_app.Application.CRUD.CredForm.DTO;
using cred_system_back_end_app.Application.CRUD.Delegate;
using cred_system_back_end_app.Application.CRUD.Insurer;
using cred_system_back_end_app.Application.CRUD.MedicalGroup.DTO;
using cred_system_back_end_app.Application.CRUD.Provider;
using cred_system_back_end_app.Application.CRUD.ProviderInsurerCompanyStatus;
using cred_system_back_end_app.Application.CRUD.ProviderInsurerCompanyStatus.DTO;
using cred_system_back_end_app.Application.CRUD.ProviderInsurerCompanyStatusHistory;
using cred_system_back_end_app.Application.CRUD.ProviderInsurerCompanyStatusHistory.DTO;
using cred_system_back_end_app.Application.UseCase.Insurer.DTO.InsurerStatus;
using cred_system_back_end_app.Application.UseCase.Notifications.NotificationManagers;
using cred_system_back_end_app.Application.UseCase.ProviderInsurerCompanyStatus.DTO;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace cred_system_back_end_app.Application.UseCase.ProviderInsurerCompanyStatus
{
    public class ProviderInsurerCompanyStatusUseCase
    {
        private readonly ProviderInsurerCompanyStatusRepository _providerInsurerCompanyStatusRepo;
        private readonly ProviderInsurerCompanyStatusHistoryRepository _providerInsurerCompanyStatusHistoryRepo;
        private readonly IMapper _mapper;
        private readonly InsurerCompanyRepository _insurerCompanyRepo;
        private readonly InsurerEmployeeRepository _insurerEmployeeRepo;
        private readonly CredFormRepository _credFormRepo;
        private readonly ProviderRepository _providerRepo;
        private readonly InsurerToProviderStatusNotificationManager _insurerToProviderStatusNotificationManager;
        private readonly InsurerToProviderStatusRTPNotificationManager _insurerToProviderStatusRTPNotificationManager;
        private readonly DelegateRepository _delegateRepository;

        public ProviderInsurerCompanyStatusUseCase(ProviderInsurerCompanyStatusRepository providerInsurerCompanyStatusRepo,
            ProviderInsurerCompanyStatusHistoryRepository providerInsurerCompanyStatusHistoryRepo,
            IMapper mapper, InsurerCompanyRepository insurerCompanyRepo, InsurerEmployeeRepository insurerEmployeeRepo,
            CredFormRepository credFormRepo, ProviderRepository providerRepo,
            InsurerToProviderStatusNotificationManager insurerToProviderStatusNotificationManager, 
            InsurerToProviderStatusRTPNotificationManager insurerToProviderStatusRTPNotificationManager, 
            DelegateRepository delegateRepository)
        {
            _providerInsurerCompanyStatusHistoryRepo = providerInsurerCompanyStatusHistoryRepo;
            _providerInsurerCompanyStatusRepo = providerInsurerCompanyStatusRepo;
            _mapper = mapper;
            _insurerCompanyRepo = insurerCompanyRepo;
            _insurerEmployeeRepo = insurerEmployeeRepo;
            _credFormRepo = credFormRepo;
            _providerRepo = providerRepo;
            _insurerToProviderStatusNotificationManager = insurerToProviderStatusNotificationManager;
            _insurerToProviderStatusRTPNotificationManager = insurerToProviderStatusRTPNotificationManager;
            _delegateRepository = delegateRepository;
        }

        //public async Task<ProviderInsurerCompanyStatusAndHistoryResponseDto> ExecuteCreatePICSAndPICSH(ProviderInsurerCompanyStatusDto pics)
        //{
        //   var picsResult = _providerInsurerCompanyStatusRepo.CreateProviderInsurerCompanyStatus(pics).Result;
        //   ProviderInsurerCompanyStatusHistoryDto picsh = new ProviderInsurerCompanyStatusHistoryDto
        //   {
        //        ProviderInsurerCompanyStatusId = picsResult.Id,
        //        InsurerStatusTypeId = picsResult.InsurerStatusTypeId,
        //        StatusDate = picsResult.CurrentStatusDate,
        //        Comment = picsResult.Comment,
        //        CommentDate = picsResult.CommentDate,
        //        CreatedBy = pics.CreatedBy
        //   };

        //   var pichsResult = _providerInsurerCompanyStatusHistoryRepo.CreateProviderInsurerCompanyStatusHistory(picsh).Result;

        //    ProviderInsurerCompanyStatusAndHistoryResponseDto response = new ProviderInsurerCompanyStatusAndHistoryResponseDto
        //    {
        //        ProviderInsurerCompanyStatusResponse = picsResult,
        //        ProviderInsurerCompanyStatusHistoryResponse = pichsResult
        //    };

        //    return response;
        //}

        //public async Task<ProviderInsurerCompanyStatusAndHistoryResponseDto> ExecuteUpdatePICSAndCreatePICSH(UpdateProviderInsurerCompanyStatusDto pics)
        //{
        //    var picsResult = _providerInsurerCompanyStatusRepo.UpdateProviderInsurerCompanyStatus(pics).Result;
        //    ProviderInsurerCompanyStatusHistoryDto picsh = new ProviderInsurerCompanyStatusHistoryDto
        //    {
        //        ProviderInsurerCompanyStatusId = picsResult.Id,
        //        InsurerStatusTypeId = picsResult.InsurerStatusTypeId,
        //        StatusDate = picsResult.CurrentStatusDate,
        //        Comment = picsResult.Comment,
        //        CommentDate = picsResult.CommentDate,
        //        CreatedBy = pics.ModifiedBy
        //    };

        //    var pichsResult = _providerInsurerCompanyStatusHistoryRepo.CreateProviderInsurerCompanyStatusHistory(picsh).Result;

        //    ProviderInsurerCompanyStatusAndHistoryResponseDto response = new ProviderInsurerCompanyStatusAndHistoryResponseDto
        //    {
        //        ProviderInsurerCompanyStatusResponse = picsResult,
        //        ProviderInsurerCompanyStatusHistoryResponse = pichsResult
        //    };

        //    return response;
        //}

        public async Task UpdatePICSAndCreatePICSH(UpdateProviderInsurerCompanyStatusDto pics)
        {
            var picsResult = await _providerInsurerCompanyStatusRepo.ModifyProviderInsurerCompanyStatus(pics);
            ProviderInsurerCompanyStatusHistoryDto picsh = new ProviderInsurerCompanyStatusHistoryDto
            {
                ProviderInsurerCompanyStatusId = picsResult.Id,
                InsurerStatusTypeId = picsResult.InsurerStatusTypeId,
                StatusDate = picsResult.CurrentStatusDate,
                Comment = picsResult.Comment,
                CommentDate = picsResult.CommentDate,
                CreatedBy = pics.ModifiedBy
            };

            var pichsResult = _providerInsurerCompanyStatusHistoryRepo.AddProviderInsurerCompanyStatusHistory(picsh);
        }

        public PicsAndPicshDTO CreatePICSAndCreatePICSH(ProviderInsurerCompanyStatusDto pics)
        {
            var picsResult =  _providerInsurerCompanyStatusRepo.AddProviderInsurerCompanyStatus(pics);
            ProviderInsurerCompanyStatusHistoryDto picsh = new ProviderInsurerCompanyStatusHistoryDto
            {
                //ProviderInsurerCompanyStatusId = picsResult.Id,
                InsurerStatusTypeId = picsResult.InsurerStatusTypeId,
                StatusDate = picsResult.CurrentStatusDate,
                Comment = picsResult.Comment,
                CommentDate = picsResult.CommentDate,
                CreatedBy = pics.CreatedBy
            };

            var picshResult =  _providerInsurerCompanyStatusHistoryRepo.AddProviderInsurerCompanyStatusHistory(picsh);

            PicsAndPicshDTO newPicsAndPicshDTO = new PicsAndPicshDTO
            {
                ProviderInsurerCompanyStatus = picsResult,
                ProviderInsurerCompanyStatusHistory = picshResult
            };

            return newPicsAndPicshDTO;

        }

        public async Task<ProviderInsurerCompanyStatusAndHistoryResponseDto> GetPICSAndPICSHByProviderAndInsurerCompany(int providerId, string email, string role)
        {
            InsurerCompanyEntity insurerCompany;
            string insurerCompanyId;
            switch (role)
            {
                case CredRole.ADMIN_INSURER: 
                    insurerCompany = await _insurerCompanyRepo.GetByAdmin(email);
                    insurerCompanyId = insurerCompany.Id;
                    break;
                case CredRole.INSURER:
                    var insurerEmployees = await _insurerEmployeeRepo.GetByEmail(email, true);
                    var insurerEmployee = insurerEmployees.FirstOrDefault();

                    if (!insurerEmployee.IsActive)
                    {
                        throw new AccessDeniedException();
                    }
                    insurerCompanyId = insurerEmployee.InsurerCompanyId;
                    break;
                default:
                    throw new AccessDeniedException();
            }

            var pics = await _providerInsurerCompanyStatusRepo.GetProviderInsurerCompanyStatusByProviderIdAndInsurerCompanyId(providerId, insurerCompanyId);
            var picsh = await _providerInsurerCompanyStatusHistoryRepo.GetProviderInsurerCompanyStatusHistoryByProviderInsurerCompanyStatusId(pics.Id);

            var picsDto = _mapper.Map<ProviderInsurerCompanyStatusResponseDto>(pics);
            picsDto.CurrentStatusDate = pics.CurrentStatusDate.ToString(DateFormats.IIPCA_DATE_FROMAT);
            picsDto.SubmitDate = pics.SubmitDate.ToString(DateFormats.IIPCA_DATE_FROMAT);
            picsDto.CommentDate = pics.CommentDate?.ToString(DateFormats.IIPCA_DATE_FROMAT);

            //var picshDto = _mapper.Map<List<ProviderInsurerCompanyStatusHistoryResponseDto>>(picsh);

            var picshDto = picsh.Select(s => new ProviderInsurerCompanyStatusHistoryResponseDto
            {
                ProviderInsurerCompanyStatusId = s.ProviderInsurerCompanyStatusId,
                InsurerStatusTypeId = s.InsurerStatusTypeId,
                StatusDate = s.StatusDate.ToString(DateFormats.IIPCA_DATE_FROMAT),
                Comment = s.Comment,
                CommentDate = s.CommentDate?.ToString(DateFormats.IIPCA_DATE_FROMAT),
                CreatedBy = s.CreatedBy
            });

            var result = new ProviderInsurerCompanyStatusAndHistoryResponseDto();
            result.ProviderInsurerCompanyStatusResponse = picsDto;
            result.ProviderInsurerCompanyStatusHistoryResponse = picshDto.ToList();

            return result;
        }

        public async Task SetStatusInsurer(SetStatusInsurerDto statusInsurer, string modifiedBy)
        {
            var dateNow = DateTime.Now;
            var pics = await _providerInsurerCompanyStatusRepo.GetProviderInsurerCompanyStatus(statusInsurer.Id);
            var providerId = pics.ProviderId;

            //Validations
            if (statusInsurer.StatusCode == StatusType.PENDING)
            {
                throw new RequestInvalidException();
            }
            if ((statusInsurer.StatusCode == StatusType.RETURNED_TO_PROVIDER || statusInsurer.StatusCode == StatusType.APPROVED)
                && pics.InsurerStatusTypeId != StatusType.PENDING)
            {
                throw new RequestInvalidException();
            }
            if (statusInsurer.StatusCode == StatusType.REJECTED
                    && pics.InsurerStatusTypeId != StatusType.RETURNED_TO_PROVIDER)
            {
                throw new RequestInvalidException();
            }
            if (statusInsurer.Comment.IsNullOrEmpty())
            {
                throw new RequestInvalidException();
            }


            var picsUpdating = _mapper.Map<UpdateProviderInsurerCompanyStatusDto>(pics);
            picsUpdating.InsurerStatusTypeId = statusInsurer.StatusCode;
            picsUpdating.CurrentStatusDate = dateNow;
            picsUpdating.SubmitDate = dateNow;
            picsUpdating.Comment = statusInsurer.Comment;
            picsUpdating.CommentDate = dateNow;
            picsUpdating.ModifiedBy = modifiedBy;


            var picsUpdated = await _providerInsurerCompanyStatusRepo.UpdateProviderInsurerCompanyStatus(picsUpdating);
            ProviderInsurerCompanyStatusHistoryDto picsh = new ProviderInsurerCompanyStatusHistoryDto
            {
                ProviderInsurerCompanyStatusId = picsUpdated.Id,
                InsurerStatusTypeId = picsUpdated.InsurerStatusTypeId,
                StatusDate = picsUpdated.CurrentStatusDate,
                Comment = picsUpdated.Comment,
                CommentDate = picsUpdated.CommentDate,
                CreatedBy = picsUpdated.ModifiedBy
            };
            await _providerInsurerCompanyStatusHistoryRepo.CreateProviderInsurerCompanyStatusHistory(picsh);

            await SetCredFormStatusByPICS(providerId, modifiedBy);

            var providerEmail = _providerRepo.GetProviderEntityById(providerId).Email;

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

        #region helpers

        public async Task SetCredFormStatusByPICS(int providerId, string modifiedBy)
        {
            var credFormId =  _providerRepo.GetProviderEntityById(providerId).CredFormId;
            var pics = await _providerInsurerCompanyStatusRepo.GetInsurerStatusesByProviderIdAsync(providerId);
            var picsRTPResult = pics.Where(r => r.InsurerStatusTypeId == StatusType.RETURNED_TO_PROVIDER).ToList();
            var picsCloseResult = pics.Where(r => r.InsurerStatusTypeId == StatusType.PENDING || r.InsurerStatusTypeId == StatusType.RETURNED_TO_PROVIDER).ToList();
            var credFormDto = new SetCredFormStatusDto();
            credFormDto.Id = credFormId;
            credFormDto.ModifiedBy = modifiedBy;

            if (picsRTPResult.Count > 0)
            {
                credFormDto.Status = StatusType.RETURNED_TO_PROVIDER;
                await _credFormRepo.SetStatusAndSave(credFormDto);
                return;
            }


            if (picsCloseResult.Count == 0)
            {
                var picsFoundAprove = pics.Where(r => r.InsurerStatusTypeId == StatusType.APPROVED).ToList();
                if (picsFoundAprove.Count > 0) { credFormDto.Status = StatusType.APPROVED; }
                else { credFormDto.Status = StatusType.REJECTED; }
                
                await _credFormRepo.SetStatusAndSave(credFormDto);
                return;
            }
        }

        private async Task SendNotificationToProviderStatus(int providerId, string providerEmail, string sentBy)
        {
            await _insurerToProviderStatusNotificationManager.SendNotificationAsync(providerId, providerEmail, sentBy);
            
        }

        private async Task SendNotificationToProviderStatusRTP(int providerId, string providerEmail, string sentBy)
        {
            await _insurerToProviderStatusRTPNotificationManager.SendNotificationAsync(providerId, providerEmail, sentBy);

        }

        private async Task SendNotificationsToDelegates(int providerId, string sentBy, bool isRTP)
        {
            var providerDelegates = await _delegateRepository.GetProviderDelegatesByProviderId(providerId);

            if (providerDelegates.IsNullOrEmpty()) return;

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
