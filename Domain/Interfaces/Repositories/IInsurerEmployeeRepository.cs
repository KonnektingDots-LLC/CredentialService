using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Domain.Interfaces.Repositories
{
    public interface IInsurerEmployeeRepository : IGenericRepository<InsurerEmployeeEntity, int>
    {
        Task<IEnumerable<InsurerEmployeeEntity>> SearchByInsurerEmployeeEmailAsync(string insurerEmployeeEmail, bool includeCompany = false);
        Task<InsurerEmployeeEntity?> GetByInsurerEmployeeEmailAsync(string email);
        Task<(List<InsurerEmployeeEntity>, int)> SearchByInsurerCompanyIdAndSearchValue(string insurerCompanyId, string searchValue, int offset = 0, int limit = 50);
        Task<(List<InsurerEmployeeEntity>, int)> SearchByInsurerCompanyId(string insurerCompanyId, int offset = 0, int limit = 50);
    }
}
