using AutoMapper;
using Azure;
using Azure.Core;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionList;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Application.Common.ResponseTO;
using cred_system_back_end_app.Application.CRUD.Document.DTO;
using cred_system_back_end_app.Application.UseCase.Submit.DTO.EducationDTOs;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using cred_system_back_end_app.Infrastructure.Settings;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace cred_system_back_end_app.Application.CRUD.Document
{
    public class DocumentCase
    {
        private DbContextEntity _context;
        private readonly IMapper _mapper;
        private BlobServiceClient _blobServiceClient;
        private readonly BlobSetting _blobSetting;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private string _email;

        public DocumentCase(DbContextEntity context, IMapper mapper,
            IOptions<BlobSetting> blobSetting, IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            //_blobService = blobServiceClient;
            _blobSetting = blobSetting.Value;
            if (!_webHostEnvironment.IsDevelopment())
            {
                _blobSetting.AzureBlobStorageKey = _configuration["AzureBlobStorageKey"];
            }
            
            _blobServiceClient = new BlobServiceClient(_blobSetting.AzureBlobStorageKey);
            
        }

        public void SetDbContextTransaction(DbContextEntity dbContextEntity)
        {
            _context = dbContextEntity;
        }

        public async Task<List<DocumentDto>> ListByContainerNameAsync(int providerId)
        {
            var container = _blobServiceClient.GetBlobContainerClient("provider-" + providerId.ToString());

            // Create a new list object for 
            List<DocumentDto> files = new List<DocumentDto>();

            await foreach (BlobItem file in container.GetBlobsAsync())
            {
                // Add each file retrieved from the storage container to the files list by creating a BlobDto object
                string uri = container.Uri.ToString();
                var name = file.Name;
                var fullUri = $"{uri}/{name}";

                files.Add(new DocumentDto
                {
                    Uri = fullUri,
                    FileName = name,
                    DocumentType = file.Properties.ContentType
                });
            }
            // Return all files to the requesting method
            return files;
        }


        public async Task<string> GenerateUniqueFileName(string filename, string documentType)
        {

            DateTime now = DateTime.Now;

            string extension = Path.GetExtension(filename).ToLower();
            string timestamp = now.ToString("yyyy-MM-ddTHHmmssfff");
            string newFileName = $"{timestamp}_{documentType}{extension}";


            return newFileName;
        }

        public async Task SetEmail(string email)
        {
            _email = email;
        }
        public async Task<DocumentResponseDto> UploadDocumentAsync(UploadDocumentDto request)
        {

            // Create new upload response object that we can return to the requesting method
            DocumentResponseDto response = new();
            var uploadDate = DateTime.Now;

            //await container.CreateAsync();
            try
            {
                var container = _blobServiceClient.GetBlobContainerClient("provider-" + request.ProviderId.ToString());
                await container.CreateIfNotExistsAsync();

                var newFileName = await GenerateUniqueFileName(request.File.FileName, request.DocumentCode);
                BlobClient client = container.GetBlobClient(newFileName);

                var documentDetails = new DocumentDetailsDto
                {
                    AzureBlobFilename = newFileName,
                    ProviderId = request.ProviderId,
                    UploadFilename = request.File.FileName,
                    Extension = Path.GetExtension(newFileName).ToLower().Substring(1),
                    ContainerName = container.Name,
                    Uri = client.Uri.AbsoluteUri,
                    SizeInBytes = request.File.Length,
                    UploadBy = _email,
                    UploadDate = uploadDate,
                    ExpirationDate = request.ExpirationDate,

                };

                //Insert
                await InsertDocumentDetails(documentDetails);


                // Open a stream for the file we want to upload
                await using (Stream? data = request.File.OpenReadStream())
                {
                    //Create Metadata
                    var metadata = new Dictionary<string, string>
                    {
                        {"OriginalFilename",request.File.FileName },
                        { "CreatedBy", _email},
                        { "CreationDate", uploadDate.ToString() },
                        { "ProviderId", request.ProviderId.ToString() },
                        { "DocumentCode", request.DocumentCode },
                        { "ExpirationDate", request.ExpirationDate.ToString() },
                        { "ChangeType", "new" },

                    };


                    // Upload the file async
                    await client.UploadAsync(data);
                    await client.SetMetadataAsync(metadata);

                }
                // Everything is OK and file got uploaded
                response.Status = $"File {request.File.FileName} Uploaded Successfully";
                response.Error = false;
                response.Document.Uri = client.Uri.AbsoluteUri;
                response.Document.FileName = client.Name;

            }

            // If the file already exists, we catch the exception and do not upload it
            catch (RequestFailedException ex)
               when (ex.ErrorCode == BlobErrorCode.BlobAlreadyExists)
            {
                //_logger.LogError($"File with name {blob.FileName} already exists in container. Set another name to store the file in the container: '{_storageContainerName}.'");
                response.Status = $"File with name {request.File.FileName} already exists. Please use another name to store your file.";
                response.Error = true;
                return response;
            }
            // If we get an unexpected error, we catch it here and return the error message
            catch (RequestFailedException ex)
            {
                // Log error to console and create a new response we can return to the requesting method
                //_logger.LogError($"Unhandled Exception. ID: {ex.StackTrace} - Message: {ex.Message}");
                response.Status = $"Unexpected error: {ex.StackTrace}. Check log with StackTrace ID.";
                response.Error = true;
                return response;
            }

            // Return the BlobUploadResponse object
            return response;
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

            var newFileName = await GenerateUniqueFileName(pdfUploadDTO.UploadFileName, documentType.Name);

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

        public void AddDocumentLocationEntity(DocumentLocationEntity documentLocationEntity)
        {
            _context.DocumentLocation.Add(documentLocationEntity);
            _context.SaveChanges();
        }

        public async Task<List<DocumentResponseDto>> UploadMultiDocumentAsync(List<UploadMultiDocumentDto> request)
        {

            // Create new upload response object that we can return to the requesting method
            List<DocumentResponseDto> responses = new();

            var container = _blobServiceClient.GetBlobContainerClient("provider-" + request[0].ProviderId);
            await container.CreateIfNotExistsAsync();

            foreach (var file in request)
            {
                var newFileName = await GenerateUniqueFileName(file.File.FileName, file.DocumentCode.ToString());
                BlobClient client = container.GetBlobClient(newFileName);

                // Open a stream for the file we want to upload
                await using (Stream? data = file.File.OpenReadStream())
                {
                    //Create Metadata
                    var metadata = new Dictionary<string, string>
                {
                    { "CreatedBy", file.UploadBy.ToString() },
                    { "CreationDate", DateTime.Now.ToString() },
                    { "ProviderId", file.ProviderId.ToString() },
                    { "DocumentCode", file.DocumentCode.ToString() }
                };


                    // Upload the file async
                    await client.UploadAsync(data);
                    await client.SetMetadataAsync(metadata);

                    //Save record y si falla borrar el record
                    //trabajar mas adelante, Json

                }

                // Everything is OK and file got uploaded
                var response = new DocumentResponseDto();

                response.Document.Uri = client.Uri.AbsoluteUri;
                response.Document.FileName = client.Name;//new filename

                //responses.Add(response);

                var r = response;

                responses.Add(r);
            }


            // Return the BlobUploadResponse object
            return responses;
        }

        public async Task<DocumentDetailsResponseDto> InsertDocumentDetails(DocumentDetailsDto request)
        {

            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {

                    var documentType = _context.DocumentType.Where(p => p.Id == request.DocumentTypeId).FirstOrDefault();
                    if (documentType == null)
                    {
                        throw new Exception($"Document type {documentType.Name} not exists");
                    }

                    request.DocumentTypeId = documentType.Id;
                    var newDocumentLocation = _mapper.Map<DocumentLocationEntity>(request);


                    await _context.DocumentLocation.AddAsync(newDocumentLocation);
                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    // Commit the transaction
                    dbContextTransaction.Commit();

                    var documentDetailsResponse = _mapper.Map<DocumentDetailsResponseDto>(newDocumentLocation);

                    return documentDetailsResponse;
                }
                catch (Exception ex)
                {


                    // Rollback the transaction
                    dbContextTransaction.Rollback();
                    throw new Exception("An error occurred inserted details of the file");

                }
            }

        }

        public async Task<DocumentDto> DownloadDocumentAsync(string blobFilename, int providerId)
        {


            try
            {
                var container = _blobServiceClient.GetBlobContainerClient("provider-" + providerId.ToString());
                // Get a reference to the blob uploaded earlier from the API in the container from configuration settings
                BlobClient file = container.GetBlobClient(blobFilename);

                // Check if the file exists in the container
                if (await file.ExistsAsync())
                {
                    var data = await file.OpenReadAsync();
                    Stream blobContent = data;

                    // Download the file details async
                    var content = await file.DownloadContentAsync();

                    // Add data to variables in order to return a BlobDto
                    string name = blobFilename;
                    string contentType = content.Value.Details.ContentType;

                    // Create new BlobDto with blob data from variables
                    return new DocumentDto { Document = blobContent, FileName = name, DocumentType = contentType };
                }
            }
            catch (RequestFailedException ex)
                when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {
                // Log error to console
                //_logger.LogError($"File {blobFilename} was not found.");
            }

            // File does not exist, return null and handle that in requesting method
            return null;
        }

        public async Task<DocumentResponseDto> DeleteDocumentAsync(string blobFilename, int providerId, string deletedBy)
        {

            var datetime = DateTime.UtcNow;
            var container = _blobServiceClient.GetBlobContainerClient("provider-" + providerId.ToString());

            BlobClient file = container.GetBlobClient(blobFilename);
            var isFileExists = await file.ExistsAsync();
            if (!isFileExists)
            {
                throw new Exception();
            }

            //delete documentDetails
            await DeleteDocumentDetails(file.Name, deletedBy, datetime);

            await file.SetAccessTierAsync(AccessTier.Cold);
            var fileProperties = await file.GetPropertiesAsync();
            fileProperties.Value.Metadata["changetype"] = "old";
            fileProperties.Value.Metadata["modifiedby"] = deletedBy;
            fileProperties.Value.Metadata["modifiedDate"] = datetime.ToString();
            await file.SetMetadataAsync(fileProperties.Value.Metadata);

            return new DocumentResponseDto();

        }

        public async Task<Empty> DeleteDocumentDetails(string filename, string modifiedBy, DateTime datetime)
        {
            try
            {
                var documentDetails = await _context.DocumentLocation.FindAsync(filename);
                documentDetails.ModifiedBy = modifiedBy;
                documentDetails.ModifiedDate = datetime;
                documentDetails.IsActive = false;

                await _context.SaveChangesAsync();
                return new Empty();
            }
            catch (RequestFailedException ex)
            {
                throw new Exception();
            }

        }

        //Is File Exists
        public async Task<bool> IsFileExistsAsync(string blobFilename, int providerId)
        {
            try
            {
                var container = _blobServiceClient.GetBlobContainerClient("provider-" + providerId.ToString());
                BlobClient file = container.GetBlobClient(blobFilename);
                bool blobExists = await file.ExistsAsync();
                return blobExists;

            }
            catch (RequestFailedException ex)
                when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {
                return false;
            }
        }

        public DocumentLocationEntity GetDocumentLocationEntityByProviderIdDocTypeFilename(int providerId, int documentTypeId, string filename)
        {
            var documentLocationEntity = _context.DocumentLocation
                                        .Where(r => r.ProviderId == providerId
                                         && r.DocumentTypeId == documentTypeId
                                         && r.UploadFilename == filename).FirstOrDefault();

            if (documentLocationEntity == null)
            {
                throw new EntityNotFoundException();
            }

            return documentLocationEntity;
        }
    }
}
