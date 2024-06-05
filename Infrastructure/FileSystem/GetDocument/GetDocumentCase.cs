using AutoMapper;
using Azure.Storage.Blobs;
using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionList;
using cred_system_back_end_app.Application.CRUD.Insurer;
using cred_system_back_end_app.Application.CRUD.MedicalGroup.DTO;
using cred_system_back_end_app.Application.CRUD.OCSAdmin;
using cred_system_back_end_app.Application.CRUD.Provider;
using cred_system_back_end_app.Application.UseCase.Delegate;
using cred_system_back_end_app.Infrastructure.B2C;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using cred_system_back_end_app.Infrastructure.FileSystem.GetDocument.DTO;
using cred_system_back_end_app.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto;
using System.IO;
using System.IO.Compression;
using System.Net.Mail;
using System.Reflection.Metadata;
using static System.Reflection.Metadata.BlobBuilder;

namespace cred_system_back_end_app.Infrastructure.FileSystem.GetDocument
{
    public class GetDocumentCase
    {

        private readonly DbContextEntity _context;
        private readonly IMapper _mapper;
        private BlobServiceClient _blobServiceClient;
        private readonly BlobSetting _blobSetting;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly InsurerCompanyRepository _insurerCompanyRepo;
        private readonly OCSAdminRepository _ocsAdminRepo;
        private readonly GetB2CInfo _getB2CInfo;
        private readonly DelegateCase _delegateCase;
        private readonly ProviderRepository _providerRepo;

        public GetDocumentCase(DbContextEntity context, IMapper mapper,
            IOptions<BlobSetting> blobSetting, IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment,
            InsurerCompanyRepository insurerCompanyRepo, OCSAdminRepository ocsAdminRepo,
            GetB2CInfo getB2CInfo, DelegateCase delegateCase, ProviderRepository providerRepo)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _blobSetting = blobSetting.Value;
            if (!_webHostEnvironment.IsDevelopment())
            {
                _blobSetting.AzureBlobStorageKey = _configuration["AzureBlobStorageKey"];
            }

            _blobServiceClient = new BlobServiceClient(_blobSetting.AzureBlobStorageKey);
            _insurerCompanyRepo = insurerCompanyRepo;
            _ocsAdminRepo = ocsAdminRepo;
            _getB2CInfo = getB2CInfo;
            _delegateCase = delegateCase;
            _providerRepo = providerRepo;
        }

        public async Task<List<DocumentByProviderResponseDto>> GetAllDocumentByProviderId(int providerId)
        {
            await ValidateProviderWithRole(providerId);

            var distinctAttestationAndFormList = await _context.DocumentLocation
                .Where(dl => dl.ProviderId == providerId && dl.IsActive == true && (dl.DocumentTypeId == 38 || dl.DocumentTypeId == 39))
                .Include(dl => dl.DocumentType)
                .Include(ps => ps.ProviderSpecialty.SpecialtyList)
                .Include(pss => pss.ProviderSubSpecialty.SubSpecialtyList)
                .GroupBy(dl  => new {dl.DocumentTypeId})
                .Select(g => g.OrderByDescending(dl => dl.UploadDate).First())
                .ToListAsync();

            var docExceptAttestationAndFormList = await _context.DocumentLocation
                .Where(dl => dl.ProviderId == providerId && dl.IsActive == true && dl.DocumentTypeId != 38 && dl.DocumentTypeId != 39)
                .Include(dl => dl.DocumentType)
                .Include(ps => ps.ProviderSpecialty.SpecialtyList)
                .Include(pss => pss.ProviderSubSpecialty.SubSpecialtyList)
                .OrderByDescending(dl => dl.UploadDate)
                .ToListAsync();

            var mergedDocLocationList = distinctAttestationAndFormList.Union(docExceptAttestationAndFormList).ToList();

            //if (mergedDocLocationList.IsNullOrEmpty())
            //{
            //    throw new DocumentNotFoundException();
            //}

            var documentByProviderResponseDto = _mapper.Map<List<DocumentByProviderResponseDto>>(mergedDocLocationList);

            return documentByProviderResponseDto;
        }
        public async Task ValidateProviderWithRole(int providerId)
        {

            InsurerCompanyEntity insurerCompany;
            IEnumerable<OCSAdminEntity> ocsAdmin;
            bool ocsAdminFound;
            var role = _getB2CInfo.Role;
            var email = _getB2CInfo.Email;
            switch (role)
            {
                case CredRole.ADMIN:
                    ocsAdmin = await _ocsAdminRepo.GetByEmailAsync(email);
                    ocsAdminFound = ocsAdmin.Any();
                    if (!ocsAdminFound) {
                        throw new OCSAdminNotFoundException(); 
                    }
                    break;
                case CredRole.ADMIN_INSURER:
                    insurerCompany = await _insurerCompanyRepo.GetByAdmin(email);
                    if (await NotPermissionForDownloadFile(insurerCompany.Id,providerId))
                    { throw new AccessDeniedException(); }
                    break;
                case CredRole.INSURER:
                    insurerCompany = await _insurerCompanyRepo.GetByEmployee(email);
                    if (await NotPermissionForDownloadFile(insurerCompany.Id, providerId))
                    { throw new AccessDeniedException(); }
                    break;
                case CredRole.DELEGATE:
                    var delegateFound = _delegateCase.GetDelegateByEmail(email);
                    if (await DelegateNotPermissionToSeeFiles(delegateFound.Id, providerId))
                    { throw new AccessDeniedException(); }
                    break;
                case CredRole.PROVIDER:
                    var ProviderFound = _providerRepo.GetProviderByEmail(email);
                    if (ProviderFound.Id != providerId)
                    { throw new AccessDeniedException(); }
                    break;                    
                default: break;

            }
        }

        public async Task<bool> DelegateNotPermissionToSeeFiles(int delegateId, int providerId)
        {
            var providerDelegateFound = _context.ProviderDelegate
                .Where(pd => pd.DelegateId == delegateId && pd.ProviderId == providerId)
                .Any();
            return !providerDelegateFound;
        }

        public async Task<bool> NotPermissionForDownloadFile(string insurerCompanyId,int providerId)
        {
            var acceptedPlanIds = await _context.InsurerCompany
                    .Include(ic => ic.AcceptedPlans)
                    .Where(ic => ic.Id == insurerCompanyId)
                    .SelectMany(ic => ic.AcceptedPlans.Select(ap => ap.Id))
                    .ToListAsync();

            var providerIds = await _context.ProviderPlanAccept
                .Where(pa => acceptedPlanIds.Contains(pa.PlanAcceptListId))
                .Select(x => x.ProviderId)
                .Distinct()
                .ToListAsync();

            var providerFound = await _context.Provider.Where(p => providerIds.Contains(providerId)).FirstOrDefaultAsync();

            return providerFound == null;
        }
        // Download specific Document Outside from Form
        public async Task<DownloadDocumentDto> DownloadDocumentAsync(DocumentSingleDownloadDto doc)
        {
            
            var documentLocation = await _context.DocumentLocation.FindAsync(doc.AzureBlobFilename);
            if (documentLocation == null)
            {
                throw new DocumentNotFoundException();
            }

            await ValidateProviderWithRole(documentLocation.ProviderId);
            var container = _blobServiceClient.GetBlobContainerClient("provider-" + documentLocation.ProviderId.ToString());
            // Get a reference to the blob uploaded earlier from the API in the container from configuration settings
            BlobClient file = container.GetBlobClient(doc.AzureBlobFilename);
            var extension = documentLocation.Extension;

            // Check if the file exists in the container
            if (await file.ExistsAsync())
            {
                var data = await file.OpenReadAsync();
                Stream blobContent = data;

                return new DownloadDocumentDto {Document = blobContent, ContentType = "application/"+extension };
            }else
            { throw new DocumentNotFoundException(); }                     
        }

        //Download Document from the Form Draft
        public async Task<DownloadDocumentDto> DownloadDocumentFromFormAsync(DocumentReviewDto documentReview)
        {
            var documentLocation = await _context.DocumentLocation
                .Where(dl => dl.ProviderId == documentReview.ProviderId
                        && dl.DocumentTypeId == documentReview.DocumentTypeId
                        && dl.UploadFilename == documentReview.UploadFilename
                        && dl.IsActive == true).FirstOrDefaultAsync();
            if (documentLocation == null)
            {
                throw new DocumentNotFoundException();
            }
            await ValidateProviderWithRole(documentLocation.ProviderId);
            var container = _blobServiceClient.GetBlobContainerClient("provider-" + documentLocation.ProviderId.ToString());
            // Get a reference to the blob uploaded earlier from the API in the container from configuration settings
            BlobClient file = container.GetBlobClient(documentLocation.AzureBlobFilename);
            var extension = documentLocation.Extension;

            // Check if the file exists in the container
            if (await file.ExistsAsync())
            {
                var data = await file.OpenReadAsync();
                Stream blobContent = data;

                return new DownloadDocumentDto { Document = blobContent, ContentType = "application/" + extension };
            }
            else
            { throw new DocumentNotFoundException(); }
        }

        // Download Batch Document by ProviderId and return a Zipfile.
        public async Task<DownloadDocumentsDto> DownloadDocumentZipByProviderIdAsync(int providerId)
        {

            //var documentLocationByProvider = await _context.DocumentLocation
            //    .Where(dl => dl.ProviderId == providerId && dl.IsActive == true)
            //    .ToListAsync();
            //if (documentLocationByProvider.IsNullOrEmpty())
            //{
            //    throw new DocumentNotFoundException();
            //}

            var documentLocationByProvider = await GetAllDocumentByProviderId(providerId);
            var ProviderFullName = await GetProviderFullName(providerId);
            string zipFilename = ProviderFullName+"_DownloadedFiles.zip";

            MemoryStream zipStream = new MemoryStream();

            using (var zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                foreach (var documentLocation in documentLocationByProvider)
                {

                    var container = _blobServiceClient.GetBlobContainerClient("provider-" + providerId.ToString());
                    // Get a reference to the blob uploaded earlier from the API in the container from configuration settings
                    BlobClient file = container.GetBlobClient(documentLocation.AzureBlobFilename);

                    // Check if the file exists in the container
                    if (await file.ExistsAsync())
                    {
                        var zipEntry = zip.CreateEntry(file.Name);

                        using (var entryStream = zipEntry.Open())
                        {
                            using (var blobStream = await file.OpenReadAsync())
                            {
                                await blobStream.CopyToAsync(entryStream);
                            }
                        }
                    }
                }                  
            }

            zipStream.Position = 0;
            return new DownloadDocumentsDto { Documents = zipStream, ContentType = "application/zip", ZipFilename = zipFilename };

        }


        // Download Selected Batch Document, request array of key(AzureBlobFilename) and return a ZipFile.
        public async Task<DownloadDocumentsDto> DownloadDocumentZipByProviderSelectionAsync(List<DocumentSelectionDto> selection)
        {
            var documentLocation = await _context.DocumentLocation.FindAsync(selection[0].AzureBlobFilename);
            if (documentLocation == null)
            {
                throw new DocumentNotFoundException();
            }

            await ValidateProviderWithRole(documentLocation.ProviderId);
            var container = _blobServiceClient.GetBlobContainerClient("provider-" + documentLocation.ProviderId.ToString());

            var ProviderFullName = await GetProviderFullName(documentLocation.ProviderId);
            string zipFilename = ProviderFullName + "_DownloadedFilesSelected.zip";

            MemoryStream zipStream = new MemoryStream();

            using (var zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                foreach (var selected in selection)
                {
                    // Get a reference to the blob uploaded earlier from the API in the container from configuration settings
                    BlobClient file = container.GetBlobClient(selected.AzureBlobFilename);

                    // Check if the file exists in the container
                    if (await file.ExistsAsync())
                    {
                        var zipEntry = zip.CreateEntry(file.Name);

                        using (var entryStream = zipEntry.Open())
                        {
                            using (var blobStream = await file.OpenReadAsync())
                            {
                                await blobStream.CopyToAsync(entryStream);
                            }
                        }
                    }
                    else
                    {
                        throw new DocumentNotFoundException();
                    }
                }
            }

            zipStream.Position = 0;
            return new DownloadDocumentsDto { Documents = zipStream, ContentType = "application/zip", ZipFilename = zipFilename };

        }

        public async Task<string> GetProviderFullName(int providerId)
        {
            var provider = await _context.Provider.FindAsync(providerId);
            if (provider == null)
            {
                throw new ProviderNotFoundException();
            }
            var result = string.IsNullOrEmpty(provider.MiddleName) 
                            ? provider.FirstName + "_" + provider.LastName
                            : provider.FirstName + "_" + provider.MiddleName + "_" + provider.LastName;
            return result;

        }

        public async Task<string> GetFormFile(int providerId)
        {
            var provider = await _context.Provider.FindAsync(providerId);
            if (provider == null)
            {
                throw new ProviderNotFoundException();
            }

            var documentLocationByProviderAndFormType = await _context.DocumentLocation
                .Where(dl => dl.ProviderId == providerId && dl.DocumentTypeId == 38 &&dl.IsActive == true)
                .FirstOrDefaultAsync();
            if (documentLocationByProviderAndFormType == null) { throw new DocumentNotFoundException(); }

            return documentLocationByProviderAndFormType.AzureBlobFilename;
        }

        public bool IsDocumentFound(DocumentReviewDto doc)
        {
            var documentLocation = _context.DocumentLocation.Where(r => r.ProviderId == doc.ProviderId
                                            && r.UploadFilename == doc.UploadFilename
                                            && r.DocumentTypeId == doc.DocumentTypeId).FirstOrDefault();

            if (documentLocation == null) {  return false; }

            var container = _blobServiceClient.GetBlobContainerClient("provider-" + doc.ProviderId.ToString());
            BlobClient file = container.GetBlobClient(documentLocation.AzureBlobFilename);

            return file.Exists();
        }
    }
}
