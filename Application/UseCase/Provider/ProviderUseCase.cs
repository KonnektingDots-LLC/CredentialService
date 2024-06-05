using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.Enum;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Application.Common.Mappers.EntityToDTO;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Application.CRUD.CredForm;
using cred_system_back_end_app.Application.CRUD.Insurer;
using cred_system_back_end_app.Application.CRUD.Provider;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace cred_system_back_end_app.Application.UseCase.Provider
{
    public class ProviderUseCase
    {
        private readonly ProviderRepository _providerRepo;
        private readonly InsurerCompanyRepository _insurerCompanyRepo;
        private readonly InsurerEmployeeRepository _insurerEmployeeRepo;
        private readonly CredFormRepository _credFormRepo;

        public ProviderUseCase(
            ProviderRepository providerRepo, 
            InsurerCompanyRepository insurerCompanyRepo,
            InsurerEmployeeRepository insurerEmployeeRepository,
            CredFormRepository credFormRepo) 
        { 
            _providerRepo = providerRepo;
            _insurerCompanyRepo = insurerCompanyRepo;
            _insurerEmployeeRepo = insurerEmployeeRepository;
            _credFormRepo = credFormRepo;
        }

        public async Task<PaginatedProviderResponseDTO> GetProvidersByRole(string role, string email, int currentPage, int limit, string? search) 
        {
            IEnumerable<ProviderEntity> providers;
            InsurerCompanyEntity insurerCompany;
            int providerCount;
            int offset;

            var paginatedResponse = new PaginatedProviderResponseDTO
            {
                CurrentPage = currentPage,
                LimitPerPage = limit,
            };
            
            switch (role)
            {
                case CredRole.ADMIN:

                    offset = PaginationHelper.GetOffset(currentPage, limit);

                    (providers, providerCount) = await _providerRepo.GetAllProviders(offset, limit, search);

                    SetPaginatedResponse(limit, providers, providerCount, paginatedResponse);

                    return paginatedResponse;

                case CredRole.ADMIN_INSURER:

                    insurerCompany = await _insurerCompanyRepo.GetByAdmin(email);

                    await SetPaginatedResponse(currentPage, limit, insurerCompany.Id, paginatedResponse, search);

                    return paginatedResponse;

                case CredRole.INSURER:

                    var insurerEmployees = await _insurerEmployeeRepo.GetByEmail(email, true);

                    if (insurerEmployees.IsNullOrEmpty()) 
                    { 
                        return paginatedResponse;
                    }

                    var insurerEmployee = insurerEmployees.FirstOrDefault();

                    if (!insurerEmployee.IsActive) 
                    {
                        throw new AccessDeniedException();
                    }

                    await SetPaginatedResponse(currentPage, limit, insurerEmployee.InsurerCompany.Id, paginatedResponse, search);

                    return paginatedResponse;

                default:

                    return paginatedResponse;
            }
        }

        private async Task SetPaginatedResponse(int currentPage, int limit, string insurerCompanyId, PaginatedProviderResponseDTO paginatedResponse,string? search) 
        {
            var offset = PaginationHelper.GetOffset(currentPage, limit);

            var (pics, providerCount) = await _providerRepo.GetProviderInsurerCompanyStatusByInsurerCompany(insurerCompanyId, offset, limit,search);

            SetPaginatedResponse(limit, pics, providerCount, paginatedResponse);
        }

        private void SetPaginatedResponse(int limit, IEnumerable<ProviderEntity> providers, int providerCount, PaginatedProviderResponseDTO paginatedResponse)
        {
            paginatedResponse.TotalNumberOfPages = (int)PaginationHelper.GetTotalNumberOfPages(limit, providerCount);

            paginatedResponse.Content = GetProviderResponseDTOs(providers);
        }

        private void SetPaginatedResponse(int limit, IEnumerable<ProviderInsurerCompanyStatusEntity> pics, int providerCount, PaginatedProviderResponseDTO paginatedResponse)
        {
            paginatedResponse.TotalNumberOfPages = (int)PaginationHelper.GetTotalNumberOfPages(limit, providerCount);

            paginatedResponse.Content = GetPICSResponseDTOs(pics);
        }

        #region helpers

        private IEnumerable<ProviderResponseBaseDTO> GetProviderResponseDTOs(IEnumerable<ProviderEntity> providers)
        {
            return providers.Select(p => p.GetProviderResponseDTO());
        }

        private IEnumerable<ProviderResponseBaseDTO> GetPICSResponseDTOs(IEnumerable<ProviderInsurerCompanyStatusEntity> pics)
        {
            return pics.Select(p => p.GetPICSResponseDTO());
        }

        #endregion
    }
}