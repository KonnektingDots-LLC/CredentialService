using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Application.Common.Mappers.EntityToDTO;
using cred_system_back_end_app.Application.DTO;
using cred_system_back_end_app.Application.DTO.Requests;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Exceptions;
using cred_system_back_end_app.Domain.Interfaces;
using MediatR;
using System.Data;

namespace cred_system_back_end_app.Application.Providers.Queries.Handlers
{
    public class GetProvidersHandler : IRequestHandler<GetProvidersQuery, PaginatedResponseBaseDto<ProviderBaseDto>>
    {
        private readonly IInsurerService _insurerService;
        private readonly ILoggerService<GetProvidersHandler> _loggerService;
        private readonly IProviderService _providerService;

        public GetProvidersHandler(IProviderService providerService, IInsurerService insurerEmployeeService, ILoggerService<GetProvidersHandler> loggerService)
        {
            _providerService = providerService;
            _insurerService = insurerEmployeeService;
            _loggerService = loggerService;
        }

        // Method called by MediatR
        public async Task<PaginatedResponseBaseDto<ProviderBaseDto>> Handle(GetProvidersQuery request, CancellationToken cancellationToken)
        {
            if (request.ProvidersRequestDto.dofilterByRole)
            {
                return await GetProvidersByRole(request.ProvidersRequestDto);
            }
            else
            {
                return await GetProvidersByEmail(request.ProvidersRequestDto.Email);
            }
        }

        private async Task<PaginatedResponseBaseDto<ProviderBaseDto>> GetProvidersByEmail(string providerEmail)
        {
            ProviderEntity? response = await _providerService.GetProviderByEmail(providerEmail);
            var paginatedResponse = new PaginatedResponseBaseDto<ProviderBaseDto>
            {
                Content = GetProviderResponseDTOs(new List<ProviderEntity>() { response })
            };

            return paginatedResponse;
        }

        private async Task<PaginatedResponseBaseDto<ProviderBaseDto>> GetProvidersByRole(GetProvidersRequestDto request)
        {
            IEnumerable<ProviderEntity> providers;
            InsurerCompanyEntity? insurerCompany;
            int providerCount;
            int offset;
            var currentPage = request.CurrentPage;
            var limitPerPage = request.LimitPerPage;
            var search = request.Search;
            var email = request.UserEmail;

            var paginatedResponse = new PaginatedResponseBaseDto<ProviderBaseDto>
            {
                CurrentPage = currentPage,
                LimitPerPage = limitPerPage,
            };

            try
            {
                switch (request.UserRole)
                {
                    case CredRole.ADMIN:
                        offset = PaginationHelper.GetOffset(currentPage, limitPerPage);
                        (providers, providerCount) = await _providerService.SearchProviders(offset, limitPerPage, search);

                        SetPaginatedResponse(limitPerPage, providers, providerCount, paginatedResponse);

                        return paginatedResponse;
                    case CredRole.ADMIN_INSURER:

                        insurerCompany = await _insurerService.GetInsurerCompanyByAdminEmail(email);
                        await SetPaginatedResponse(currentPage, limitPerPage, insurerCompany.Id.ToString(), paginatedResponse, search);

                        return paginatedResponse;

                    case CredRole.INSURER:
                        insurerCompany = await _insurerService.GetInsurerCompanyByEmployeeEmail(email);
                        await SetPaginatedResponse(currentPage, limitPerPage, insurerCompany.Id.ToString(), paginatedResponse, search);

                        return paginatedResponse;

                    default:

                        return paginatedResponse;
                }
            }
            catch (GenericInsurerException insurerException)
            {
                _loggerService.Warn(insurerException.Message);
                // return an empty content response (only basic pagination) after logging the error (This is a business exception)
                return paginatedResponse;
            }
            catch (Exception ex)
            {
                _loggerService.Error($"An unhandled exception ocurred: {ex.Message}", ex);
                // return an empty content response (only basic pagination) after logging the exception.
                return paginatedResponse;
            }
        }

        private async Task SetPaginatedResponse(int currentPage, int limit, string insurerCompanyId, PaginatedResponseBaseDto<ProviderBaseDto> paginatedResponse, string? search)
        {
            var offset = PaginationHelper.GetOffset(currentPage, limit);

            var (pics, providerCount) = await _providerService.SearchProviderInsurerCompanyStatusesByInsurerCompany(insurerCompanyId, offset, limit, search);

            SetPaginatedResponse(limit, pics, providerCount, paginatedResponse);
        }

        private void SetPaginatedResponse(int limit, IEnumerable<ProviderEntity> providers, int providerCount, PaginatedResponseBaseDto<ProviderBaseDto> paginatedResponse)
        {
            paginatedResponse.TotalNumberOfPages = (int)PaginationHelper.GetTotalNumberOfPages(limit, providerCount);

            paginatedResponse.Content = GetProviderResponseDTOs(providers);
        }

        private void SetPaginatedResponse(int limit, IEnumerable<ProviderInsurerCompanyStatusEntity> pics, int providerCount, PaginatedResponseBaseDto<ProviderBaseDto> paginatedResponse)
        {
            paginatedResponse.TotalNumberOfPages = (int)PaginationHelper.GetTotalNumberOfPages(limit, providerCount);

            paginatedResponse.Content = GetPICSResponseDTOs(pics);
        }

        #region helpers

        private IEnumerable<ProviderBaseDto> GetProviderResponseDTOs(IEnumerable<ProviderEntity> providers)
        {
            return providers.Select(p => p.GetProviderResponseDTO());
        }

        private IEnumerable<ProviderBaseDto> GetPICSResponseDTOs(IEnumerable<ProviderInsurerCompanyStatusEntity> pics)
        {
            return pics.Select(p => p.GetPICSResponseDTO());
        }

        #endregion
    }
}
