using Azure.Storage.Blobs;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Exceptions;
using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Domain.Services.DTO;
using cred_system_back_end_app.Domain.Services.Helpers;

namespace cred_system_back_end_app.Domain.Services
{
    public class MultiFileUploadService : IMultiFileUploadService
    {
        private readonly IFileStorageClient _blobStorageClient;
        private readonly IDocumentLocationRepository _documentLocationRepository;
        private readonly IGenericRepository<DocumentTypeEntity, int> _documentTypeRepository;
        private readonly IProviderDetailRepository _providerDetailRepository;
        private readonly IJsonProviderFormRepository _jsonProviderFormRepository;

        private readonly IGenericRepository<BoardDocumentEntity, int> _boardDocumentRepository;
        private readonly IGenericRepository<MedicalSchoolDocumentEntity, int> _medicalSchoolDocumentRepository;
        private readonly IGenericRepository<CorporationDocumentEntity, int> _corporationDocumentRepository;
        private readonly IGenericRepository<EducationInfoDocumentEntity, int> _educationInfoDocumentRepository;
        private readonly IGenericRepository<ProviderSpecialtyEntity, int> _providerSpecialtyRepository;
        private readonly IGenericRepository<ProviderSubSpecialtyEntity, int> _providerSubSpecialtyRepository;
        private readonly IGenericRepository<JsonProviderFormHistoryEntity, int> _jsonProviderFormHistoryRepository;

        private readonly IProviderService _providerService;

        private bool replaceFile = false;
        private readonly long minSizeInBytes = 1000;
        private readonly long maxSizeInBytes = 5000000;

        public MultiFileUploadService(IFileStorageClient fileStorageClient, IDocumentLocationRepository documentLocationRepository,
            IGenericRepository<DocumentTypeEntity, int> documentTypeRepository, IProviderDetailRepository providerDetailRepository,
            IJsonProviderFormRepository jsonProviderFormRepository, IProviderService providerService,
            IGenericRepository<BoardDocumentEntity, int> boardDocumentRepository,
            IGenericRepository<MedicalSchoolDocumentEntity, int> medicalSchoolDocumentRepository,
            IGenericRepository<CorporationDocumentEntity, int> corporationDocumentRepository,
            IGenericRepository<EducationInfoDocumentEntity, int> educationInfoDocumentRepository,
            IGenericRepository<ProviderSpecialtyEntity, int> providerSpecialtyRepository,
            IGenericRepository<ProviderSubSpecialtyEntity, int> providerSubSpecialtyRepository,
            IGenericRepository<JsonProviderFormHistoryEntity, int> jsonProviderFormHistoryRepository)
        {
            _blobStorageClient = fileStorageClient;
            _documentLocationRepository = documentLocationRepository;
            _documentTypeRepository = documentTypeRepository;
            _providerDetailRepository = providerDetailRepository;
            _jsonProviderFormRepository = jsonProviderFormRepository;
            _providerService = providerService;
            _boardDocumentRepository = boardDocumentRepository;
            _medicalSchoolDocumentRepository = medicalSchoolDocumentRepository;
            _corporationDocumentRepository = corporationDocumentRepository;
            _educationInfoDocumentRepository = educationInfoDocumentRepository;
            _providerSpecialtyRepository = providerSpecialtyRepository;
            _providerSubSpecialtyRepository = providerSubSpecialtyRepository;
            _jsonProviderFormHistoryRepository = jsonProviderFormHistoryRepository;
        }

        public async Task AddJsonProviderForm(string json, int providerId)
        {
            // Json Provider Form
            var modifiedBy = _documentLocationRepository.GetLoggedUserEmail();
            var modifiedDate = DateTime.Now;
            var existingJsonRecord = await _jsonProviderFormRepository.GetLatestByProviderId(providerId);


            if (existingJsonRecord == null)
            {
                //Add New
                JsonProviderFormEntity newProviderDraft = new()
                {
                    ProviderId = providerId,
                    JsonBody = json,
                    CreatedBy = modifiedBy, //Get from B2C
                    CreationDate = modifiedDate
                };
                await _jsonProviderFormRepository.AddAndSaveAsync(newProviderDraft);
            }
            else
            {

                //Update
                existingJsonRecord.ModifiedDate = modifiedDate;
                existingJsonRecord.ModifiedBy = modifiedBy;
                existingJsonRecord.JsonBody = json;
                await _jsonProviderFormRepository.UpdateAsync(existingJsonRecord);
            }

            JsonProviderFormHistoryEntity newProviderDraftHistory = new JsonProviderFormHistoryEntity()
            {
                ProviderId = providerId,
                JsonBody = json,
                CreatedBy = modifiedBy,
                CreationDate = modifiedDate,
            };

            await _jsonProviderFormHistoryRepository.AddAndSaveAsync(newProviderDraftHistory);

        }


        #region upload multi documents
        public async Task<List<MultiFileUploadResponseDto>> UploadMultiDocumentAsync(List<MultiFileUploadDto> request, int providerId)
        {

            string changeTypeValue = "new";
            // Create new upload response object that we can return to the requesting method
            List<MultiFileUploadResponseDto> responses = new();
            var uploadDate = DateTime.Now;

            _ = await _providerService.GetProviderById(providerId) ?? throw new ProviderNotFoundException();
            var container = await _blobStorageClient.CreateIfNotExistsAsync("provider-" + providerId);

            foreach (var file in request)
            {
                var fileSize = file.File.Length;
                if (fileSize < minSizeInBytes || fileSize >= maxSizeInBytes) { throw new FileSizeException(); }

                replaceFile = string.IsNullOrEmpty(file.OldFilename) ? false : true;

                var documentRecordFound = await _documentLocationRepository.GetByProviderIdAndDocumentTypeAndUploadfilename(providerId, file.DocumentTypeId, file.File.FileName);

                if (documentRecordFound == null || replaceFile)
                {
                    var documentType = await _documentTypeRepository.GetByIdAsync(file.DocumentTypeId)
                    ?? throw new DocumentTypeException($"Document type was not found by id [{file.DocumentTypeId}]");

                    var newFileName = GenerateUniqueFileName(file.File.FileName, documentType.Name);
                    BlobClient client = container.GetBlobClient(newFileName);


                    var documentLocation = DocumentLocationHelper.GetDocumentLocationEntity(newFileName, file,
                    container.Name, client, uploadDate, providerId);

                    await ProcessInsertionBySection(file, documentLocation);

                    // Open a stream for the file we want to upload
                    await using (Stream? data = file.File.OpenReadStream())
                    {
                        //Create Metadata
                        var metadata = new Dictionary<string, string>
                    {
                        { "CreatedBy", _documentLocationRepository.GetLoggedUserEmail() }, //B2C
                        { "CreationDate", DateTime.Now.ToString() },
                        { "ProviderId", providerId.ToString() },
                        { "DocumentTypeName",  documentType.Name },
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
                    var r = response;

                    responses.Add(r);
                }
            }

            return responses;
        }

        private string GenerateUniqueFileName(string filename, string documentType)
        {

            DateTime now = DateTime.Now;

            string extension = Path.GetExtension(filename).ToLower();
            string timestamp = now.ToString("yyyy-MM-ddTHHmmssfff");
            string newFileName = $"{timestamp}_{documentType}{extension}";


            return newFileName;
        }

        private async Task ProcessInsertionBySection(MultiFileUploadDto file, DocumentLocationEntity newDocumentLocation)
        {
            var documentType = await _documentTypeRepository.GetByIdAsync(file.DocumentTypeId);
            switch (documentType.DocumentSectionTypeId)
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
                throw new BadHttpRequestException("NPI is Required.");
            }
            if (!newDocumentLocation.ExpirationDate.HasValue && newDocumentLocation.DocumentTypeId == 2)
            {
                throw new BadHttpRequestException("Issue Date is Required.");
            }
            if (!newDocumentLocation.ExpirationDate.HasValue && newDocumentLocation.DocumentTypeId == 2)
            {
                throw new BadHttpRequestException("Expiration Date is Required.");
            }

            await InsertCommonData(newDocumentLocation);

        }

        private async Task InsertCitizentypeData(DocumentLocationEntity newDocumentLocation)
        {

            if (!newDocumentLocation.ExpirationDate.HasValue)
            {
                throw new BadHttpRequestException("Expiration Date is Required.");
            }

            await InsertCommonData(newDocumentLocation);

        }

        private async Task InsertForeignerData(DocumentLocationEntity newDocumentLocation)
        {

            if (!newDocumentLocation.ExpirationDate.HasValue && newDocumentLocation.DocumentTypeId == 9)
            {
                throw new BadHttpRequestException("Expiration Date is Required.");
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
                throw new BadHttpRequestException("Issue Date is Required.");
            }

            if (!newDocumentLocation.ExpirationDate.HasValue)
            {
                throw new BadHttpRequestException("Expiration Date is Required.");
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

        private async Task InsertCommonData(DocumentLocationEntity newDocumentLocation)
        {
            if (replaceFile)
            {
                var removeDocumentDetails = await _documentLocationRepository.GetByProviderIdAndDocumentTypeAndUploadfilename(
                    newDocumentLocation.ProviderId,
                    newDocumentLocation.DocumentTypeId,
                    newDocumentLocation.UploadFilename)
                    ?? throw new DocumentNotFoundException(newDocumentLocation.ProviderId, newDocumentLocation.DocumentTypeId, newDocumentLocation.UploadFilename);

                var providerDetail = await _providerDetailRepository.GetByProviderId(newDocumentLocation.ProviderId);
                if (providerDetail != null)
                {
                    await ProcessDeletionRelationShipBySection(removeDocumentDetails);
                }
                await _documentLocationRepository.DeleteAsync(removeDocumentDetails);

                await _blobStorageClient.DeleteDocumentAsync(removeDocumentDetails.ProviderId, removeDocumentDetails.AzureBlobFilename);

            }

            await _documentLocationRepository.AddAndSaveAsync(newDocumentLocation);
        }
        #endregion

        #region delete file

        public async Task DeleteFilesAsync(List<FileToDeleteDto> filesToDelete, int providerId)
        {
            foreach (var fileToDeleteDto in filesToDelete)
            {
                var recordToDelete = await _documentLocationRepository.GetByProviderIdAndDocumentTypeAndUploadfilename(providerId, fileToDeleteDto.DocumentTypeId, fileToDeleteDto.UploadFilename)
                    ?? throw new DocumentNotFoundException($"Document was not found by providerId " +
                    $"[{providerId}] DocumentTypeId [{fileToDeleteDto.DocumentTypeId}] and UploadFilename [{fileToDeleteDto.UploadFilename}]");

                _ = await _documentTypeRepository.GetByIdAsync(recordToDelete.DocumentTypeId)
                    ?? throw new DocumentTypeException($"Document type was not found by id [{recordToDelete.DocumentTypeId}]");

                await _blobStorageClient.DeleteDocumentAsync(providerId, recordToDelete.AzureBlobFilename);
                await DeleteDocumentLocationAsync(recordToDelete);
            }
        }

        public async Task DeleteDocumentLocationAsync(DocumentLocationEntity documentLocation)
        {
            var providerDetail = await _providerDetailRepository.GetByProviderId(documentLocation.ProviderId);
            if (providerDetail != null)
            {
                await ProcessDeletionRelationShipBySection(documentLocation);
            }
            await _documentLocationRepository.DeleteAsync(documentLocation);
        }

        public async Task ProcessDeletionRelationShipBySection(DocumentLocationEntity documentLocation)
        {
            var documentType = await _documentTypeRepository.GetByIdAsync(documentLocation.DocumentTypeId)
                    ?? throw new DocumentTypeException($"Document type was not found by id [{documentLocation.DocumentTypeId}]");

            switch (documentType.DocumentSectionTypeId)
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
            await _corporationDocumentRepository.DeleteIfFoundAsync(r => r.DocumentLocationId == documentLocation.AzureBlobFilename);
        }

        private async Task DeleteSpecialtyData(DocumentLocationEntity documentLocation)
        {
            await _providerSpecialtyRepository.DeleteIfFoundAsync(r => r.ProviderId == documentLocation.ProviderId
                                        && r.AzureBlobFileName == documentLocation.AzureBlobFilename);
        }

        private async Task DeleteSubSpecialtyData(DocumentLocationEntity documentLocation)
        {
            await _providerSubSpecialtyRepository.DeleteIfFoundAsync(r => r.ProviderId == documentLocation.ProviderId
                                        && r.DocumentLocationId == documentLocation.AzureBlobFilename);
        }

        private async Task DeleteEducationData(DocumentLocationEntity documentLocation)
        {
            await _educationInfoDocumentRepository.DeleteIfFoundAsync(r => r.AzureBlobFilename == documentLocation.AzureBlobFilename);
        }

        private async Task DeleteMedicalSchoolData(DocumentLocationEntity documentLocation)
        {
            await _medicalSchoolDocumentRepository.DeleteIfFoundAsync(r => r.AzureBlobFilename == documentLocation.AzureBlobFilename);
        }


        private async Task DeleteBoardData(DocumentLocationEntity documentLocation)
        {
            await _boardDocumentRepository.DeleteIfFoundAsync(r => r.AzureBlobFilename == documentLocation.AzureBlobFilename);
        }


        #endregion
    }
}
