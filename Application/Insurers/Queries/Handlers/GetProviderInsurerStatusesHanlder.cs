using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using MediatR;

namespace cred_system_back_end_app.Application.Insurers.Queries.Handlers
{
    public class GetProviderInsurerStatusesHanlder : IRequestHandler<GetProviderInsurerStatusesQuery, PaginatedBaseNonListContentResponseDto<ProviderInsurerStatusResponseDto>>
    {
        private readonly IProviderInsurerCompanyStatusRepository _repository;

        public GetProviderInsurerStatusesHanlder(IProviderInsurerCompanyStatusRepository providerInsurerCompanyStatusRepository)
        {
            _repository = providerInsurerCompanyStatusRepository;
        }

        public Task<PaginatedBaseNonListContentResponseDto<ProviderInsurerStatusResponseDto>> Handle(GetProviderInsurerStatusesQuery request, CancellationToken cancellationToken)
        {
            return GetProviderInsurerStatuses(request.ProviderId, request.CurrentPage, request.LimitPerPage);
        }

        public async Task<PaginatedBaseNonListContentResponseDto<ProviderInsurerStatusResponseDto>> GetProviderInsurerStatuses
        (
            int providerId,
            int currentPage,
            int limitPerPage
        )
        {
            var offset = PaginationHelper.GetOffset(currentPage, limitPerPage);

            var (statuses, totalNumberOfRecords) = await _repository.SearchInsurerStatusesByProviderIdAsync(providerId, offset, limitPerPage);

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

            return new PaginatedBaseNonListContentResponseDto<ProviderInsurerStatusResponseDto>
            {
                CurrentPage = currentPage,
                LimitPerPage = limitPerPage,
                TotalNumberOfPages = totalNumberOfPages,
                Content = new ProviderInsurerStatusResponseDto
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
    }
}
