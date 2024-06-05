using AutoMapper;
using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Application.Common.RequestDto;
using cred_system_back_end_app.Application.CRUD.Insurer;
using cred_system_back_end_app.Application.CRUD.Provider;
using cred_system_back_end_app.Application.CRUD.ProviderInsurerCompanyStatus;
using cred_system_back_end_app.Application.UseCase.Insurer.DTO.InsurerStatus;
using cred_system_back_end_app.Application.UseCase.Insurer.DTO.UpdateInsurerEmployee;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.IdentityModel.Tokens;

namespace cred_system_back_end_app.Application.UseCase.Insurer
{
    public class InsurerUseCase
    {
        private readonly InsurerAdminRepository _insurerAdminRepo;
        private readonly ProviderRepository _providerRepo;
        private readonly InsurerEmployeeRepository _insurerEmployeeRepo;
        private readonly ProviderInsurerCompanyStatusRepository _providerInsurerCompanyStatusRepo;
        private readonly IMapper _mapper;

        public InsurerUseCase(InsurerAdminRepository insurerAdminRepo, 
            ProviderRepository providerRepo,
            InsurerEmployeeRepository insurerEmployeeRepo,
            ProviderInsurerCompanyStatusRepository providerInsurerCompanyStatusRepo,
            IMapper mapper)
        {
            //TODO: configure dependency injection to use singleton for dbContext,
            // so that every repo uses the same dbContext.
            _insurerAdminRepo = insurerAdminRepo;
            _providerRepo = providerRepo;
            _insurerEmployeeRepo = insurerEmployeeRepo;
            _providerInsurerCompanyStatusRepo = providerInsurerCompanyStatusRepo;
            _mapper = mapper;
        }

        public async Task<bool> ValidateAdmin(string email)
        {
            var insurerAdmin = await _insurerAdminRepo.GetByEmail(email);

            return insurerAdmin.Any();
        }

        public async Task UpdateInsurerAdmin(string email, Names names)
        {
            await _insurerAdminRepo.UpdateInsurerAdmin(email, names);
        }

        public async Task<(IEnumerable<InsurerEmployeeEntity>, int)> GetEmployees(int currentPage, int limitPerPage, string insurerAdminEmail)
        {
            var offset = GetOffset(currentPage, limitPerPage);

            return await _insurerEmployeeRepo.GetByInsurerAdminEmail(offset, limitPerPage, insurerAdminEmail);
        }

        public async Task<(IEnumerable<InsurerEmployeeEntity>, int)> GetSearchEmployees(int currentPage, int limitPerPage, string insurerAdminEmail, string search)
        {
            var offset = GetOffset(currentPage, limitPerPage);

            return await _insurerEmployeeRepo.GetBySearchInsurerAdminEmail(offset, limitPerPage, insurerAdminEmail,search);
        }

        public async Task UpdateInsurerEmployee(UpdateInsurerEmployeeRequestDto updateInsurerEmployeeDto)
        {
            await _insurerEmployeeRepo.UpdateInsurerEmployee(updateInsurerEmployeeDto);
        }

        public async Task SetEmployeeStatusAsync(bool isActive, string email) 
        {
            await _insurerEmployeeRepo.UpdateInsurerEmployeeAsync(isActive, email);
        }

        public async Task<bool> ValidateInsurerEmployee(string email) 
        {
            var employees = await _insurerEmployeeRepo.GetByEmail(email);

            return !employees.IsNullOrEmpty();            
        }

        public async Task<PaginatedProviderInsurerStatusResponseDTO> GetProviderInsurerStatuses
        (
            int providerId, 
            int currentPage, 
            int limitPerPage
        )
        {
            var offset = PaginationHelper.GetOffset(currentPage, limitPerPage);

            var (statuses, totalNumberOfRecords) = await _providerInsurerCompanyStatusRepo
                .GetInsurerStatusesByProviderIdAsync(providerId, offset, limitPerPage);

            var providerInsurerResponseDTOS = statuses.Select(s => new ProviderInsurerStatusDTO
            {
                Name = s.InsurerCompany.Name,
                CurrentStatusDate = s.CurrentStatusDate.ToString(DateFormats.IIPCA_DATE_FROMAT),
                Status = s.InsurerStatusType.Name,
                Note = s.Comment,
                NoteDate = s.CommentDate?.ToString(DateFormats.IIPCA_DATE_FROMAT)                
            });

            var totalNumberOfPages = (int)PaginationHelper.GetTotalNumberOfPages(limitPerPage, totalNumberOfRecords);

            var providerData = statuses.Select(x => x.Provider).FirstOrDefault();


            // NOTE: currently, we are assuming that the latest "SinceDate"
            // of all PENDING insurers is the date of the latest submit.
            // in the future, we should store the date of latest submit separately.
            var picsPendingExists = statuses.Where(x => x.InsurerStatusTypeId == StatusType.PENDING).Any();
            DateTime lastSubmitDate = DateTime.Now;
            if (picsPendingExists)
            {
                lastSubmitDate = statuses
                    .Where(x => x.InsurerStatusTypeId == StatusType.PENDING)
                    .Select(x => x.SubmitDate)
                    .Max();
            }
            else
            {
                lastSubmitDate = statuses
                    .Select(x => x.SubmitDate)
                    .Max();
            }

            return new PaginatedProviderInsurerStatusResponseDTO
            {
                CurrentPage = currentPage,
                LimitPerPage = limitPerPage,
                TotalNumberOfPages = totalNumberOfPages,
                Content = new ProviderInsurerStatusResponseDTO
                {
                    ProviderId = providerData.Id,
                    Name = providerData.FirstName,
                    LastName = providerData.LastName,
                    MiddleName = providerData.MiddleName,
                    Surname = providerData.SurName,
                    RenderingNPI = providerData.RenderingNPI,
                    Summary = new Summary
                    {
                        LastSubmitDate = lastSubmitDate.ToString(DateFormats.IIPCA_DATE_FROMAT)
                    },
                    InsurerStatusList = providerInsurerResponseDTOS
                }
            };
        }

        #region helpers

        private int GetOffset(int currentPage, int limit)
        {
            if (currentPage < 0)
            {
                throw new AggregateException("currentPage must be >= 1");
            }

            return (currentPage - 1) * limit;
        }

        #endregion
    }
}
