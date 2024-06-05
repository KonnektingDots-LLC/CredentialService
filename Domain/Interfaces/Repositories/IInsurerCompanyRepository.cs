using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Domain.Interfaces.Repositories
{
    public interface IInsurerCompanyRepository : IGenericRepository<InsurerCompanyEntity, string>
    {
        Task<InsurerCompanyEntity?> GetByInsurerAdminEmailAsync(string? insurerAdminEmail);
        Task<InsurerCompanyEntity> GetByEmployee(string email);
        Task<IEnumerable<InsurerCompanyEntity>> GetByProvider(int providerId);
    }
}
