using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Domain.Interfaces.Repositories
{
    public interface IProviderRepository : IGenericRepository<ProviderEntity, int>
    {
        Task<(List<ProviderEntity>, int)> SearchAllAsync(int offset = 0, int limit = 50);
        Task<(List<ProviderEntity>, int)> SearchByFullNameAsync(string fullname, int offset = 0, int limit = 50);
        Task<(List<ProviderEntity>, int)> SearchByNpiAsync(string npiNumber, int offset = 0, int limit = 50);
        Task<ProviderEntity?> GetByEmailAsync(string email);
    }
}
