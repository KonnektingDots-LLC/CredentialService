using Azure.Storage.Blobs;
using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Domain.Settings;
using Microsoft.Extensions.Options;

namespace cred_system_back_end_app.Infrastructure.AzureBlobStorage
{
    public class FileStorageClient : IFileStorageClient
    {
        private readonly BlobServiceClient _blobServiceClient;

        public FileStorageClient(IOptions<BlobSetting> blobSetting, IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment)
        {
            var _blobSetting = blobSetting.Value;
            if (!webHostEnvironment.IsDevelopment())
            {
                _blobSetting.AzureBlobStorageKey = configuration["AzureBlobStorageKey"];
            }

            _blobServiceClient = new BlobServiceClient(_blobSetting.AzureBlobStorageKey);
        }

        public async Task<BlobContainerClient> CreateIfNotExistsAsync(string containerName)
        {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);
            await container.CreateIfNotExistsAsync();
            return container;
        }

        public async Task DeleteDocumentAsync(int providerId, string azureFilename)
        {
            var container = _blobServiceClient.GetBlobContainerClient("provider-" + providerId.ToString());
            BlobClient file = container.GetBlobClient(azureFilename);
            var isFileExists = await file.ExistsAsync();
            if (isFileExists)
            {
                await file.DeleteAsync();
            }

        }
    }
}
