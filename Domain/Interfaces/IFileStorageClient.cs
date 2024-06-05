using Azure.Storage.Blobs;

namespace cred_system_back_end_app.Domain.Interfaces
{
    public interface IFileStorageClient
    {
        Task DeleteDocumentAsync(int providerId, string azureFilename);
        Task<BlobContainerClient> CreateIfNotExistsAsync(string containerName);
    }
}
