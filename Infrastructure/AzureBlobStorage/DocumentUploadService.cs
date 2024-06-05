using Azure.Storage.Blobs;
using cred_system_back_end_app.Application.DTO.Documents;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Exceptions;
using cred_system_back_end_app.Domain.Settings;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.Extensions.Options;

namespace cred_system_back_end_app.Infrastructure.AzureBlobStorage
{
    public class DocumentUploadService
    {
        private DbContextEntity _context;
        private readonly BlobServiceClient _blobServiceClient;

        public DocumentUploadService(DbContextEntity context,
            IOptions<BlobSetting> blobSetting, IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            var _blobSetting = blobSetting.Value;
            if (!webHostEnvironment.IsDevelopment())
            {
                _blobSetting.AzureBlobStorageKey = configuration["AzureBlobStorageKey"];
            }

            _blobServiceClient = new BlobServiceClient(_blobSetting.AzureBlobStorageKey);

        }

        public void SetDbContextTransaction(DbContextEntity dbContextEntity)
        {
            _context = dbContextEntity;
        }

        public async Task<PdfDocumentResponse> UploadPDFAsync(PdfUploadDto pdfUploadDTO)
        {
            // Create new upload response object that we can return to the requesting method
            var uploadDate = pdfUploadDTO.UploadDate;

            //await container.CreateAsync();
            var container = _blobServiceClient.GetBlobContainerClient("provider-" + pdfUploadDTO.ProviderId.ToString());
            await container.CreateIfNotExistsAsync();

            var documentType = _context.DocumentType.Where(dt => dt.Id == pdfUploadDTO.DocumentTypeId).FirstOrDefault();
            if (documentType == null) { throw new DocumentTypeException(); }

            var newFileName = GenerateUniqueFileName(pdfUploadDTO.UploadFileName, documentType.Name);

            BlobClient client = container.GetBlobClient(newFileName);

            var documentLocationEntity = new DocumentLocationEntity
            {
                AzureBlobFilename = newFileName,
                ProviderId = pdfUploadDTO.ProviderId,
                UploadFilename = pdfUploadDTO.UploadFileName,
                Extension = Path.GetExtension(newFileName).ToLower().Substring(1),
                DocumentTypeId = pdfUploadDTO.DocumentTypeId,
                ContainerName = container.Name,
                Uri = client.Uri.AbsoluteUri,
                SizeInBytes = pdfUploadDTO.PdfStream.Length,
                UploadBy = pdfUploadDTO.UploadBy,
                UploadDate = uploadDate,
            };

            AddDocumentLocationEntity(documentLocationEntity);

            //Create Metadata
            var metadata = new Dictionary<string, string>
            {
                {"OriginalFilename",pdfUploadDTO.UploadFileName},
                { "CreatedBy", pdfUploadDTO.UploadBy },
                { "CreationDate", uploadDate.ToString() },
                { "ProviderId", pdfUploadDTO.ProviderId.ToString() },
                { "DocumentCode",  documentType.Name},
                { "ChangeType", "new" },

            };

            // Upload the file async
            await client.UploadAsync(pdfUploadDTO.PdfStream);
            await client.SetMetadataAsync(metadata);

            var response = new PdfDocumentResponse();
            response.Filename = client.Name;

            // Return the BlobUploadResponse object
            return response;
        }

        private void AddDocumentLocationEntity(DocumentLocationEntity documentLocationEntity)
        {
            _context.DocumentLocation.Add(documentLocationEntity);
            _context.SaveChanges();
        }

        private string GenerateUniqueFileName(string filename, string documentType)
        {

            DateTime now = DateTime.Now;

            string extension = Path.GetExtension(filename).ToLower();
            string timestamp = now.ToString("yyyy-MM-ddTHHmmssfff");
            string newFileName = $"{timestamp}_{documentType}{extension}";


            return newFileName;
        }
    }
}
