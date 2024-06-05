using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Domain.Interfaces
{
    public interface IInsurerService
    {
        Task<InsurerCompanyEntity> GetInsurerCompanyByEmployeeEmail(string insurerEmployeeEmail);
        Task<InsurerCompanyEntity> GetInsurerCompanyByAdminEmail(string insurerAdminEmail);
        Task<InsurerAdminEntity?> CreateInsurerEmployee(string? newInsurerEmployeeEmail, string? insurerAdminEmail);
        Task<(IEnumerable<InsurerEmployeeEntity>, int)> SearchByInsurerCompanyId(int currentPage, int limitPerPage, string insurerCompanyId, string? search);
        Task SetInsurerEmployeeStatusByEmailAsync(bool isActive, string email);
    }
}
