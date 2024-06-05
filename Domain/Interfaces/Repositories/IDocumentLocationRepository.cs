using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Domain.Interfaces.Repositories
{
    public interface IDocumentLocationRepository
    {
        Task<DocumentLocationEntity?> GetByProviderIdAndDocumentTypeAndUploadfilename(int providerId, int documentType, string uploadFilename);
        Task<DocumentLocationEntity> AddAndSaveAsync(DocumentLocationEntity entity);
        Task<DocumentLocationEntity> UpdateAsync(DocumentLocationEntity entity);
        Task DeleteAsync(DocumentLocationEntity entity);
        string? GetLoggedUserEmail();
    }
}
