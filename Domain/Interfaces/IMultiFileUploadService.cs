using cred_system_back_end_app.Domain.Services.DTO;

namespace cred_system_back_end_app.Domain.Interfaces
{
    public interface IMultiFileUploadService
    {
        Task DeleteFilesAsync(List<FileToDeleteDto> filesToDelete, int providerId);
        Task<List<MultiFileUploadResponseDto>> UploadMultiDocumentAsync(List<MultiFileUploadDto> request, int ProviderId);
        Task AddJsonProviderForm(string json, int providerId);
    }
}
