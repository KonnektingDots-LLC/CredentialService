using AutoMapper;
using Azure.Storage.Blobs;
using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionList;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Application.CRUD.Document.DTO;
using cred_system_back_end_app.Application.CRUD.MedicalGroup.DTO;
using cred_system_back_end_app.Application.CRUD.Provider;
using cred_system_back_end_app.Application.CRUD.ProviderDraft.DTO;
using cred_system_back_end_app.Application.UseCase.SaveJsonDraft;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using cred_system_back_end_app.Infrastructure.FileSystem.MultiFileUpload.DTO;
using cred_system_back_end_app.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Xml.Linq;

namespace cred_system_back_end_app.Infrastructure.FileSystem.MultiFileUpload
{
    public class MultiFileUploadCase
    {

        private readonly DbContextEntity _context;
        private readonly IMapper _mapper;
        private BlobServiceClient _blobServiceClient;
        private readonly BlobSetting _blobSetting;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly SaveJsonDraftCase _saveJsonDraftCase;
        private readonly ProviderRepository _providerRepo;
        private bool replaceFile = false;
        private string _email;
        private DateTime transactionDate;
        private long minSizeInBytes = 1000;
        private long maxSizeInBytes = 5000000;

        public MultiFileUploadCase(DbContextEntity context, IMapper mapper,
            IOptions<BlobSetting> blobSetting, IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment,SaveJsonDraftCase saveJsonDraftCase,
            ProviderRepository providerRepo)
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
            _saveJsonDraftCase = saveJsonDraftCase;
            _providerRepo = providerRepo;

        }
        public async Task SetEmail(string email)
        {
            _email = email;
        }
        public async Task<Empty> ProcessDocumentJsonProvider(List<MultiFileUploadDto> FileDetail, string Json, int ProviderId, 
            string? filesToDelete)
        {
            transactionDate = DateTime.Now;
            Empty response = new();
            if (ProviderId == 0) { throw new ProviderNotFoundException(); }

            if (!string.IsNullOrEmpty(filesToDelete))
            {
                List<FileToDeleteDto> filesToDeleteList = JsonConvert.DeserializeObject<List<FileToDeleteDto>>(filesToDelete);
            
                if (filesToDeleteList.Count > 0)
                {
                    await DeleteFilesAsync(filesToDeleteList, ProviderId);
                }
            }

            if (FileDetail.Count > 0) 
            {
                await UploadMultiDocumentAsync(FileDetail, ProviderId);
            }

            ProviderDraftDto providerDraft = new ProviderDraftDto()
            {
                ProviderId = ProviderId,
                JsonBody = Json
            };
            
            await AddJsonProviderForm(providerDraft.JsonBody,providerDraft.ProviderId,_email);
            await ContextSave();

            return response;
        }
        private async Task AddJsonProviderForm(string json, int providerId, string modifiedBy)
        {
            // Json Provider Form

            var modifiedDate = transactionDate;
            var existingJsonRecord = await _context.JsonProviderForm.Where(x => x.ProviderId == providerId).FirstOrDefaultAsync();


            if (existingJsonRecord == null)
            {
                //Add New
                ProviderDraftDto providerDraft = new ProviderDraftDto()
                {
                    ProviderId = providerId,
                    JsonBody = json
                };
                var newProviderDraft = _mapper.Map<JsonProviderFormEntity>(providerDraft);
                newProviderDraft.CreatedBy = modifiedBy; //Get from B2C
                newProviderDraft.CreationDate = modifiedDate;
                _context.JsonProviderForm.Add(newProviderDraft);
            }
            else
            {

                //Update
                existingJsonRecord.ModifiedDate = modifiedDate;
                existingJsonRecord.ModifiedBy = modifiedBy;
                existingJsonRecord.JsonBody = json;
                _context.Entry(existingJsonRecord).State = EntityState.Modified;

            }

            JsonProviderFormHistoryEntity newProviderDraftHistory = new JsonProviderFormHistoryEntity()
            {
                ProviderId = providerId,
                JsonBody = json,
                CreatedBy = modifiedBy,
                CreationDate = modifiedDate,
            };

            _context.JsonProviderFormHistory.Add(newProviderDraftHistory);

        }
        private async Task DeleteFilesAsync(List<FileToDeleteDto> filesToDelete, int providerId)
        {
            foreach (var fileToDeleteDto in filesToDelete)
            {

                var recordToDelete = _context.DocumentLocation
                    .Where(drf => drf.ProviderId == providerId
                    && drf.UploadFilename == fileToDeleteDto.UploadFilename
                    && drf.DocumentTypeId == fileToDeleteDto.DocumentTypeId).FirstOrDefault();

                var DocumentType = _context.DocumentType.Where(dt => dt.Id == recordToDelete.DocumentTypeId).FirstOrDefault();

                if (DocumentType == null)
                {
                    throw new DocumentTypeException();
                }

                if (recordToDelete == null) { throw new DocumentNotFoundException(); }

                await deleteDocumentAsync(providerId, recordToDelete.AzureBlobFilename);

                await DeleteDocumentLocationAsync(recordToDelete);
                                
                await _context.SaveChangesAsync();

            }
        }

        public async Task DeleteDocumentLocationAsync(DocumentLocationEntity documentLocation)
        {
            var providerDetail = await _providerRepo.GetProviderDetailByProviderId(documentLocation.ProviderId);
            if(providerDetail != null)
            {
                await ProcessDeletionRelationShipBySection(documentLocation);
            }
            _context.DocumentLocation.Remove(documentLocation);
        }

        public async Task ProcessDeletionRelationShipBySection(DocumentLocationEntity documentLocation)
        {
            int documentSectionTypeId = _context.DocumentType.Find(documentLocation.DocumentTypeId).DocumentSectionTypeId;
            switch (documentSectionTypeId)
            {

                case 4:
                    await DeleteCorporationData(documentLocation);
                    break;
                case 5:
                    await DeleteSpecialtyData(documentLocation);
                    break;
                case 6:
                    await DeleteSubSpecialtyData(documentLocation);
                    break;
                case 9:
                    await DeleteEducationData(documentLocation);
                    break;
                case 10:
                    await DeleteMedicalSchoolData(documentLocation);
                    break;
                case 12:
                    await DeleteBoardData(documentLocation);
                    break;

            }
        }

        private async Task DeleteCorporationData(DocumentLocationEntity documentLocation)
        {
            var corporationDocument = _context.CorporationDocument
                           .Where(r => r.DocumentLocationId == documentLocation.AzureBlobFilename).FirstOrDefault();

            if (corporationDocument != null)
            {
                _context.CorporationDocument.Remove(corporationDocument);
            }
            
        }

        private async Task DeleteSpecialtyData(DocumentLocationEntity documentLocation)
        {
            var providerSpecialty =  _context.ProviderSpecialty
                                        .Where(r => r.ProviderId == documentLocation.ProviderId
                                        && r.AzureBlobFileName == documentLocation.AzureBlobFilename).FirstOrDefault();

            if (providerSpecialty != null)
            {
                _context.ProviderSpecialty.Remove(providerSpecialty);
            }          
        }

        private async Task DeleteSubSpecialtyData(DocumentLocationEntity documentLocation)
        {
            var providerSubSpecialty = _context.ProviderSubSpecialty
                                        .Where(r => r.ProviderId == documentLocation.ProviderId
                                        && r.DocumentLocationId == documentLocation.AzureBlobFilename).FirstOrDefault();

            if (providerSubSpecialty != null)
            {
                _context.ProviderSubSpecialty.Remove(providerSubSpecialty);
            }

        }

        private async Task DeleteEducationData(DocumentLocationEntity documentLocation)
        {
            var educationInfoDocument = _context.EducationInfoDocument
               .Where(r => r.AzureBlobFilename == documentLocation.AzureBlobFilename).FirstOrDefault();


            if (educationInfoDocument != null)
            {
                _context.EducationInfoDocument.Remove(educationInfoDocument);
            }
        }

        private async Task DeleteMedicalSchoolData(DocumentLocationEntity documentLocation)
        {
            var medicalSchoolDocument = _context.MedicalSchoolDocument
                .Where(r => r.AzureBlobFilename == documentLocation.AzureBlobFilename).FirstOrDefault(); ;


            if (medicalSchoolDocument != null)
            {
                _context.MedicalSchoolDocument.Remove(medicalSchoolDocument);
            }
        }


        private async Task DeleteBoardData(DocumentLocationEntity documentLocation)
        {
            var boardDocument = _context.BoardDocument
            .Where(r => r.AzureBlobFilename == documentLocation.AzureBlobFilename).FirstOrDefault();


            if (boardDocument != null)
            {
                _context.BoardDocument.Remove(boardDocument);
            }
        }

        private async Task DeleteProfessionalLiabilityData(DocumentLocationEntity documentLocation)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MultiFileUploadResponseDto>> UploadMultiDocumentAsync(List<MultiFileUploadDto> request, int ProviderId)
        {

            string changeTypeValue = "new";
            // Create new upload response object that we can return to the requesting method
            List<MultiFileUploadResponseDto> responses = new();
            var uploadDate = DateTime.Now;

            var providerExists = await _context.Provider.FindAsync(ProviderId);
            if (providerExists == null) { throw new ProviderNotFoundException(); }

            var container = _blobServiceClient.GetBlobContainerClient("provider-" + ProviderId);
            await container.CreateIfNotExistsAsync();

            foreach (var file in request)
            {
                var fileSize = file.File.Length;
                if(fileSize < minSizeInBytes || fileSize >= maxSizeInBytes) { throw new FileSizeException(); }

                replaceFile = string.IsNullOrEmpty(file.OldFilename) ? false : true;

                var documentRecordFound = _context.DocumentLocation.Where(
                     drf => drf.UploadFilename == file.File.FileName
                 && drf.DocumentTypeId == file.DocumentTypeId
                 && drf.ProviderId == ProviderId
                 ).FirstOrDefault();

                if (documentRecordFound == null || replaceFile == true)
                {
                    var DocumentType = _context.DocumentType.Where(dt => dt.Id == file.DocumentTypeId).FirstOrDefault();

                    if (DocumentType == null)
                    {
                        throw new DocumentTypeException();
                    }

                    var newFileName = await GenerateUniqueFileName(file.File.FileName, DocumentType.Name);
                    BlobClient client = container.GetBlobClient(newFileName);
                 

                    var documentLocation = DocumentLocationHelper.GetDocumentLocationEntity(newFileName, file,
                    container, client, uploadDate,ProviderId);

                    documentLocation.UploadBy = _email;

         
                    await ProcessInsertionBySection(file, documentLocation);

                    // Open a stream for the file we want to upload
                    await using (Stream? data = file.File.OpenReadStream())
                    {
                        //Create Metadata
                        var metadata = new Dictionary<string, string>
                    {
                        { "CreatedBy", _email }, //B2C
                        { "CreationDate", transactionDate.ToString() },
                        { "ProviderId", ProviderId.ToString() },
                        { "DocumentTypeName",  DocumentType.Name },
                        { "ExpirationDate", file.ExpirationDate.ToString() },
                        { "issueDate", file.IssueDate.ToString() },
                        { "letterDate", file.LetterDate.ToString() },
                        { "ChangeType", changeTypeValue},
                    };


                        // Upload the file async
                        await client.UploadAsync(data);
                        await client.SetMetadataAsync(metadata);

                    }

                    // Everything is OK and file got uploaded
                    var response = new MultiFileUploadResponseDto();

                    response.NewFileName = newFileName;
                    response.UserFileName = file.File.FileName;

                    //responses.Add(response);

                    var r = response;

                    responses.Add(r);
                }
            }

            return responses;
        }

        private async Task<string> GenerateUniqueFileName(string filename, string documentType)
        {

            DateTime now = DateTime.Now;

            string extension = Path.GetExtension(filename).ToLower();
            string timestamp = now.ToString("yyyy-MM-ddTHHmmssfff");
            string newFileName = $"{timestamp}_{documentType}{extension}";


            return newFileName;
        }

        private async Task ProcessInsertionBySection(MultiFileUploadDto file, DocumentLocationEntity newDocumentLocation)
        {
            int documentSectionTypeId = _context.DocumentType.Find(file.DocumentTypeId).DocumentSectionTypeId;
            switch (documentSectionTypeId)
            {
                case 1:
                    await InsertIndividualData(newDocumentLocation);
                    break;
                case 2:
                    await InsertCitizentypeData(newDocumentLocation);
                    break;
                case 3:
                    await InsertForeignerData(newDocumentLocation);
                    break;
                case 4:
                    await InsertCorporationData(newDocumentLocation);
                    break;
                case 5:
                    await InsertSpecialtyData(newDocumentLocation);
                    break;
                case 6:
                    await InsertSubSpecialtyData(newDocumentLocation);
                    break;
                case 7:
                    await InsertPCPData(newDocumentLocation);
                    break;
                case 8:
                    await Insert330Data(newDocumentLocation);
                    break;
                case 9:
                    await InsertEducationData(newDocumentLocation);
                    break;
                case 10:
                    await InsertMedicalSchoolData(newDocumentLocation);
                    break;
                case 11:
                    await InsertMedicalLicenseData(newDocumentLocation);
                    break;
                case 12:
                    await InsertBoardData(newDocumentLocation);
                    break;
                case 13:
                    await InsertCriminalRecordData(newDocumentLocation);
                    break;
                case 14:
                    await InsertMalpracticeData(newDocumentLocation);
                    break;
                case 15:
                    await InsertProfessionalLiabilityData(newDocumentLocation);
                    break;
                default: throw new NotImplementedException();
            }

        }


        private async Task InsertIndividualData(DocumentLocationEntity newDocumentLocation)
        {

            if (string.IsNullOrEmpty(newDocumentLocation.NPI) && newDocumentLocation.DocumentTypeId == 1)
            {
                throw new Exception("NPI is Required.");
            }
            if (!newDocumentLocation.ExpirationDate.HasValue && newDocumentLocation.DocumentTypeId == 2)
            {
                throw new Exception("Issue Date is Required.");
            }
            if (!newDocumentLocation.ExpirationDate.HasValue && newDocumentLocation.DocumentTypeId == 2)
            {
                throw new Exception("Expiration Date is Required.");
            }

            await InsertCommonData(newDocumentLocation);

        }

        private async Task InsertCitizentypeData(DocumentLocationEntity newDocumentLocation)
        {

            if (!newDocumentLocation.ExpirationDate.HasValue)
            {
                throw new Exception("Expiration Date is Required.");
            }

            await InsertCommonData(newDocumentLocation);

        }

        private async Task InsertForeignerData(DocumentLocationEntity newDocumentLocation)
        {

            if (!newDocumentLocation.ExpirationDate.HasValue && newDocumentLocation.DocumentTypeId == 9)
            {
                throw new Exception("Expiration Date is Required.");
            }
            await InsertCommonData(newDocumentLocation);
        }

        private async Task InsertCorporationData(DocumentLocationEntity newDocumentLocation)
        {
            await InsertCommonData(newDocumentLocation);
        }

        private async Task InsertSpecialtyData(DocumentLocationEntity newDocumentLocation)
        {
            await InsertCommonData(newDocumentLocation);
        }

        private async Task InsertSubSpecialtyData(DocumentLocationEntity newDocumentLocation)
        {
            await InsertCommonData(newDocumentLocation);
        }

        private async Task InsertPCPData(DocumentLocationEntity newDocumentLocation)
        {

            await InsertCommonData(newDocumentLocation);
        }

        private async Task Insert330Data(DocumentLocationEntity newDocumentLocation)
        {
            await InsertCommonData(newDocumentLocation);
        }

        private async Task InsertEducationData(DocumentLocationEntity newDocumentLocation)
        {
            await InsertCommonData(newDocumentLocation);
        }

        private async Task InsertMedicalSchoolData(DocumentLocationEntity newDocumentLocation)
        {
            await InsertCommonData(newDocumentLocation);
        }

        private async Task InsertMedicalLicenseData(DocumentLocationEntity newDocumentLocation)
        {
            await InsertCommonData(newDocumentLocation);
        }

        private async Task InsertBoardData(DocumentLocationEntity newDocumentLocation)
        {
            await InsertCommonData(newDocumentLocation);
        }

        private async Task InsertCriminalRecordData(DocumentLocationEntity newDocumentLocation)
        {
            if (!newDocumentLocation.IssueDate.HasValue)
            {
                throw new Exception("Issue Date is Required.");
            }

            if (!newDocumentLocation.ExpirationDate.HasValue)
            {
                throw new Exception("Expiration Date is Required.");
            }
            await InsertCommonData(newDocumentLocation);
        }

        private async Task InsertMalpracticeData(DocumentLocationEntity newDocumentLocation)
        {
            await InsertCommonData(newDocumentLocation);
        }

        private async Task InsertProfessionalLiabilityData(DocumentLocationEntity newDocumentLocation)
        {
            await InsertCommonData(newDocumentLocation);
        }       
        private async Task deleteDocumentAsync(int providerId, string azureFilename)
        {
            var container = _blobServiceClient.GetBlobContainerClient("provider-" + providerId.ToString());
            BlobClient file = container.GetBlobClient(azureFilename);
            var isFileExists = await file.ExistsAsync();
            if (isFileExists)
            {
                await file.DeleteAsync();
            }          

        }

        private async Task InsertCommonData(DocumentLocationEntity newDocumentLocation)
        {
            if (replaceFile)
            {
                var removeDocumentDetails = _context.DocumentLocation.Where(
                    d => d.UploadFilename == newDocumentLocation.OldFilename
                    && d.DocumentTypeId == newDocumentLocation.DocumentTypeId
                    && d.ProviderId == newDocumentLocation.ProviderId
                ).FirstOrDefault();

                if (removeDocumentDetails == null)
                {
                    throw new Exception("Document not found");
                }
                var providerDetail = await _providerRepo.GetProviderDetailByProviderId(newDocumentLocation.ProviderId);
                if (providerDetail != null)
                {
                    await ProcessDeletionRelationShipBySection(removeDocumentDetails);
                }
                _context.DocumentLocation.Remove(removeDocumentDetails);

                await deleteDocumentAsync(removeDocumentDetails.ProviderId, removeDocumentDetails.AzureBlobFilename);

            }

                _context.DocumentLocation.Add(newDocumentLocation);
        }

        private async Task ContextSave()
        {
            using (var dbTransaction = _context.Database.BeginTransaction())
            {
                await _context.SaveChangesAsync();
                await dbTransaction.CommitAsync();
            }
        }
    }
}
