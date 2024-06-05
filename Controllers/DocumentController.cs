using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.CRUD.Document;
using cred_system_back_end_app.Application.CRUD.Document.DTO;
using cred_system_back_end_app.Application.UseCase.SaveJsonDraft;
using cred_system_back_end_app.Infrastructure.B2C;
using cred_system_back_end_app.Infrastructure.FileSystem.GetDocument;
using cred_system_back_end_app.Infrastructure.FileSystem.GetDocument.DTO;
using cred_system_back_end_app.Infrastructure.FileSystem.MultiFileUpload;
using cred_system_back_end_app.Infrastructure.FileSystem.MultiFileUpload.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Extensions.Msal;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using System.Text.Json;

namespace cred_system_back_end_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly DocumentCase _documentCase;
        private readonly MultiFileUploadCase _multiFileUploadCase;
        private readonly GetDocumentCase _getDocumentCase;
        private readonly GetB2CInfo _getB2CInfo;


        public DocumentController(DocumentCase documentCase, MultiFileUploadCase multiFileUploadCase,
            GetDocumentCase getDocumentCase, GetB2CInfo getB2CInfo)
        {
            _documentCase = documentCase;
            _multiFileUploadCase = multiFileUploadCase;
            _getDocumentCase = getDocumentCase;
            _getB2CInfo = getB2CInfo;
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet(nameof(GetAllByProviderId))]
        public async Task<IActionResult> GetAllByProviderId(int request)
        {
            // Get all files at the Azure Storage Location and return them
            List<DocumentDto>? files = await _documentCase.ListByContainerNameAsync(request);

            // Returns an empty array if no files are present at the storage container
            return Ok(files);
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("Upload")]
        public async Task<IActionResult> UploadDocument([FromForm] UploadDocumentDto request)
        {
            await _documentCase.SetEmail(User.FindFirst("emails")?.Value);
            var response = await _documentCase.UploadDocumentAsync(request);
            return Ok(response);
        }

        [Authorize(Policy = CredPolicy.ACCESS_AS_PD)]
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("MultiUpload")]
        public async Task<IActionResult> MultiUploadDocument([FromForm] List<MultiFileUploadDto> fileDetail, [FromForm] string json,
            [FromForm] int providerId, [FromForm] string? filesToDelete)
        {
            
            await _multiFileUploadCase.SetEmail(User.FindFirst("emails")?.Value);
            //save Document and json
            var response = await _multiFileUploadCase.ProcessDocumentJsonProvider(fileDetail,json,providerId, filesToDelete);
            return Ok(response);
        }

        [Authorize(Policy=CredPolicy.ACCESS_AS_ALL)]
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("Download")]
        public async Task<IActionResult> DownloadDocument(DocumentSingleDownloadDto doc)
        {
            _getB2CInfo.Role = User.FindFirst(CredTokenKey.ROLE)?.Value;
            _getB2CInfo.Email = User.FindFirst(CredTokenKey.EMAIL)?.Value;
            DownloadDocumentDto? file = await _getDocumentCase.DownloadDocumentAsync(doc);
            return File(file.Document, file.ContentType);
        }

        [Authorize(Policy = CredPolicy.ACCESS_AS_PD)]
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("DownloadReview")]
        public async Task<IActionResult> DownloadDocumentReview(DocumentReviewDto documentReview)
        {
            DownloadDocumentDto? file = await _getDocumentCase.DownloadDocumentFromFormAsync(documentReview);
            return File(file.Document, file.ContentType);
        }

        [Authorize(Policy = CredPolicy.ACCESS_AS_ALL)]
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("DownloadZipByProvider")]
        public async Task<IActionResult> DownloadDocumentsByProvider(int providerId)
        {
            _getB2CInfo.Role = User.FindFirst(CredTokenKey.ROLE)?.Value;
            _getB2CInfo.Email = User.FindFirst(CredTokenKey.EMAIL)?.Value;
            DownloadDocumentsDto? file = await _getDocumentCase.DownloadDocumentZipByProviderIdAsync(providerId);
            return File(file.Documents, file.ContentType,file.ZipFilename);
        }

        [Authorize(Policy = CredPolicy.ACCESS_AS_ALL)]
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("DownloadZipByProviderSelection")]
        public async Task<IActionResult> DownloadDocumentsByProviderSelection(List<DocumentSelectionDto> selection)
        {
            _getB2CInfo.Role = User.FindFirst(CredTokenKey.ROLE)?.Value;
            _getB2CInfo.Email = User.FindFirst(CredTokenKey.EMAIL)?.Value;
            DownloadDocumentsDto? file = await _getDocumentCase.DownloadDocumentZipByProviderSelectionAsync(selection);
            return File(file.Documents, file.ContentType, file.ZipFilename);
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpDelete("{filename}/{providerId}")]
        public async Task<IActionResult> Delete(string filename, int providerId, string deletedby)
        {
            DocumentResponseDto response = await _documentCase.DeleteDocumentAsync(filename, providerId, User.FindFirst("emails")?.Value);


            // File has been successfully deleted
            return StatusCode(StatusCodes.Status200OK, "deleted");

        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("IsExists/{filename}/{providerId}")]
        public async Task<IActionResult> IsExists(string filename, int providerId)
        {
            var response = await _documentCase.IsFileExistsAsync(filename, providerId);
            return Ok(response);
        }

        [Authorize(Policy = CredPolicy.ACCESS_AS_ALL)]
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("DocumentByProvider")]
        public async Task<IActionResult> GetDocumentByProvider(int providerId)
        {
            _getB2CInfo.Role = User.FindFirst(CredTokenKey.ROLE)?.Value;
            _getB2CInfo.Email = User.FindFirst(CredTokenKey.EMAIL)?.Value;
            var response = await _getDocumentCase.GetAllDocumentByProviderId(providerId);
            return Ok(response);
        }

        [Authorize(Policy = CredPolicy.ACCESS_AS_ALL)]
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("GetFormByProvider")]
        public async Task<IActionResult> GetFormFileByProvider(int providerId)
        {
            var response = await _getDocumentCase.GetFormFile(providerId);
            return Ok(response);
        }

        [Authorize(Policy = CredPolicy.ACCESS_AS_PD)]
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("IsDocumentFound")]
        public IActionResult IsDocumentFound(DocumentReviewDto doc)
        {
            var isDocumentFound = _getDocumentCase.IsDocumentFound(doc);
            return Ok(isDocumentFound);
        }
    }
}
