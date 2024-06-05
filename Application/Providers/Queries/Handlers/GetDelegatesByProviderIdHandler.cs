using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Application.DTO;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using MediatR;

namespace cred_system_back_end_app.Application.Providers.Queries.Handlers
{
    public class GetDelegatesByProviderIdHandler : IRequestHandler<GetDelegatesByProviderIdQuery, PaginatedResponseBaseDto<DelegateInfoDto>>
    {
        private readonly IProviderDelegatesRepository _providerDelegatesRepository;

        public GetDelegatesByProviderIdHandler(IProviderDelegatesRepository providerDelegatesRepository)
        {
            _providerDelegatesRepository = providerDelegatesRepository;
        }

        public async Task<PaginatedResponseBaseDto<DelegateInfoDto>> Handle(GetDelegatesByProviderIdQuery request, CancellationToken cancellationToken)
        {
            var offset = PaginationHelper.GetOffset(request.CurrentPage, request.LimitPerPage);
            var (providerDelegates, delegateCount) = await _providerDelegatesRepository.SearchByProviderId(request.ProviderId, offset, request.LimitPerPage);

            var delegateList = new PaginatedResponseBaseDto<DelegateInfoDto>()
            {
                CurrentPage = request.CurrentPage,
                LimitPerPage = request.LimitPerPage,
                TotalNumberOfPages = (int)PaginationHelper.GetTotalNumberOfPages(request.LimitPerPage, delegateCount)
            };

            if (!providerDelegates.Any())
            {
                delegateList.Content = new DelegateInfoDto[] { };
                return delegateList;
            }

            var delegateInfoDTOs = providerDelegates.Select(p => new DelegateInfoDto
            {
                Id = p.DelegateId,
                FullName = p.Delegate.FullName,
                Email = p.Delegate.Email,
                IsActive = p.IsActive,
            })
            .ToArray();

            delegateList.Content = delegateInfoDTOs;

            return delegateList;
        }
    }
}
