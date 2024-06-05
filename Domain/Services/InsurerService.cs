using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Exceptions;
using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace cred_system_back_end_app.Domain.Services
{
    public class InsurerService : IInsurerService
    {
        private readonly IInsurerCompanyRepository _insurerCompanyRepository;
        private readonly IInsurerEmployeeRepository _insurerEmployeeRepository;
        private readonly IInsurerAdminRepository _insurerAdminRepository;

        public InsurerService(IInsurerCompanyRepository insurerCompanyRepository, IInsurerEmployeeRepository insurerEmployeeRepository, IInsurerAdminRepository insurerAdminRepository)
        {
            _insurerCompanyRepository = insurerCompanyRepository;
            _insurerEmployeeRepository = insurerEmployeeRepository;
            _insurerAdminRepository = insurerAdminRepository;
        }

        /// <summary>
        /// Create a insurer employee.
        /// </summary>
        /// <param name="newInsurerEmployeeEmail"></param>
        /// <param name="insurerAdminEmail"></param>
        /// <returns></returns>
        /// <exception cref="InsurerAdminNotFoundException"></exception>
        /// <exception cref="InsurerCompanyNotFoundException"></exception>
        public async Task<InsurerAdminEntity?> CreateInsurerEmployee(string? newInsurerEmployeeEmail, string? insurerAdminEmail)
        {
            var insurerAdmin = await _insurerAdminRepository.GetByEmailAsync(insurerAdminEmail)
                ?? throw new InsurerAdminNotFoundException("Insurer admin was not found by email.");
            if (insurerAdmin.InsurerCompanyId == null)
            {
                throw new InsurerCompanyNotFoundException($"Insurer company was not found by insurer admin id {insurerAdmin.Id}");
            }
            var insurerCompanyFound = await _insurerCompanyRepository.GetByIdAsync(insurerAdmin.InsurerCompanyId)
                ?? throw new InsurerCompanyNotFoundException($"Insurer company was not found by insurer company id {insurerAdmin.InsurerCompany.Id}");
            var insurerEmployeeFound = await _insurerEmployeeRepository.SearchByInsurerEmployeeEmailAsync(newInsurerEmployeeEmail);
            if (insurerEmployeeFound == null)
            {
                var insurerEmployee = new InsurerEmployeeEntity
                {
                    Email = newInsurerEmployeeEmail,
                    InsurerCompanyId = insurerCompanyFound.Id.ToString(),
                    CreatedBy = insurerAdminEmail
                };

                await _insurerEmployeeRepository.AddAndSaveAsync(insurerEmployee);
            }

            return insurerAdmin;
        }

        /// <summary>
        /// Search for a insurer company by insurer admin email
        /// </summary>
        /// <param name="insurerAdminEmail"></param>
        /// <returns></returns>
        /// <exception cref="InsurerEmployeeNotFoundException"></exception>
        public async Task<InsurerCompanyEntity> GetInsurerCompanyByAdminEmail(string insurerAdminEmail)
        {
            var insurerCompany = await _insurerCompanyRepository.GetByInsurerAdminEmailAsync(insurerAdminEmail);
            if (insurerCompany == null)
            {
                throw new InsurerEmployeeNotFoundException($"Insurer company was not found.");
            }

            return insurerCompany;
        }

        /// <summary>
        /// Get an insurer company by insurer employee email
        /// </summary>
        /// <param name="insurerEmployeeEmail"></param>
        /// <returns></returns>
        /// <exception cref="InsurerEmployeeNotFoundException"></exception>
        /// <exception cref="GenericInsurerException"></exception>
        public async Task<InsurerCompanyEntity> GetInsurerCompanyByEmployeeEmail(string insurerEmployeeEmail)
        {
            IEnumerable<InsurerEmployeeEntity> insurerEmployeesEntity = await _insurerEmployeeRepository.SearchByInsurerEmployeeEmailAsync(insurerEmployeeEmail, includeCompany: true);
            if (insurerEmployeesEntity.IsNullOrEmpty())
            {
                throw new InsurerEmployeeNotFoundException("Insurer employee was not found.");
            }

            var insurerEmployee = insurerEmployeesEntity.First();

            if (!insurerEmployee.IsActive)
            {
                throw new GenericInsurerException($"Insurer employee {insurerEmployee.Id} is not active.");
            }

            return insurerEmployee.InsurerCompany;
        }

        /// <summary>
        /// Search by insurer company id and search value.
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="limitPerPage"></param>
        /// <param name="insurerCompanyId"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        /// <exception cref="GenericInsurerException"></exception>
        public async Task<(IEnumerable<InsurerEmployeeEntity>, int)> SearchByInsurerCompanyId(int currentPage, int limitPerPage, string insurerCompanyId, string? search)
        {
            if (currentPage < 0)
            {
                throw new GenericInsurerException("currentPage must be >= 1");
            }

            int offset = (currentPage - 1) * limitPerPage;
            if (search != null)
            {
                var searchValue = search.Replace(" ", "");
                return await _insurerEmployeeRepository.SearchByInsurerCompanyIdAndSearchValue(insurerCompanyId, searchValue, offset, limitPerPage);
            }
            else
            {
                return await _insurerEmployeeRepository.SearchByInsurerCompanyId(insurerCompanyId, currentPage, limitPerPage);
            }
        }

        /// <summary>
        /// Set insurer employee status by email.
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="InsurerEmployeeNotFoundException"></exception>
        public async Task SetInsurerEmployeeStatusByEmailAsync(bool isActive, string email)
        {
            var employee = await _insurerEmployeeRepository.GetByInsurerEmployeeEmailAsync(email)
                ?? throw new InsurerEmployeeNotFoundException("No employee matches the given email.");

            employee.IsActive = isActive;
            await _insurerEmployeeRepository.UpdateAsync(employee);
        }
    }
}
