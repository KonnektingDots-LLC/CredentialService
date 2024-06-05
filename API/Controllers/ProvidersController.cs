using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.DTO.Documents;
using cred_system_back_end_app.Application.DTO.Requests;
using cred_system_back_end_app.Application.Insurers.Queries;
using cred_system_back_end_app.Application.Providers.Commands;
using cred_system_back_end_app.Application.Providers.Queries;
using cred_system_back_end_app.Domain.Services.DTO;
using cred_system_back_end_app.Infrastructure.AzureBlobStorage;
using cred_system_back_end_app.Infrastructure.B2C;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace cred_system_back_end_app.API.Controllers
{
    [Route("api/providers")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly DocumentDownloadService _documentDownloadService;
        private readonly GetB2CInfo _getB2CInfo;

        public ProvidersController(IMediator mediator, DocumentDownloadService documentDownloadService, GetB2CInfo getB2CInfo)
        {
            _mediator = mediator;
            _documentDownloadService = documentDownloadService;
            _getB2CInfo = getB2CInfo;
        }

        #region provider detail

        /// <summary>
        /// Get a filtered list of providers with pagination.
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="limitPerPage"></param>
        /// <param name="dofilterByRole"></param>
        /// <param name="email"></param>
        /// <param name="search"></param>
        /// <returns>A list of providers with pagination</returns>
        /// <response code="200">Successful request.</response>
        [HttpGet]
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] int currentPage = 1, [FromQuery] int limitPerPage = 50, [FromQuery] bool dofilterByRole = false,
            [FromQuery] string? search = null, [FromQuery] string? email = null)
        {
            GetProvidersRequestDto requestDto = new()
            {
                CurrentPage = currentPage,
                LimitPerPage = limitPerPage,
                Search = search,
                UserEmail = User.FindFirst(CredTokenKey.EMAIL)?.Value,
                UserRole = User.FindFirst(CredTokenKey.ROLE)?.Value,
                Email = email,
                dofilterByRole = dofilterByRole
            };

            return Ok(await _mediator.Send(new GetProvidersQuery(requestDto)));
        }

        /// <summary>
        /// Create a new provider, and atach a new credentialing form.
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns>The newly created provider.</returns>
        /// <response code="200">Successful request.</response>
        /// <response code="400">Provider type id is invalid</response>
        [HttpPost]
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateProviderRequestDto createDto)
        {
            createDto.Email = User.FindFirst("emails")?.Value;
            var response = await _mediator.Send(new CreateProviderCommand(createDto));
            return Ok(response);
        }

        #endregion

        #region credentialing-forms

        /// <summary>
        /// search a credentialing form status by provider email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("{id}/credentialing-forms")]
        public async Task<IActionResult> GetProviderByEmail(string email)
        {
            var response = await _mediator.Send(new GetCredentialingFormStatusByProviderEmailQuery(email));
            return Ok(response);
        }

        /// <summary>
        /// Generate and upload a credentialing form for a provider.
        /// </summary>
        /// <param name="submitData"></param>
        /// <returns></returns>
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("{id}/credentialing-forms")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> SubmitAll(SubmitRequestDTO submitData)
        {
            var email = User.FindFirst(CredTokenKey.EMAIL)?.Value;
            var pdfDocumentResponse = await _mediator.Send(new SubmitCredentialingFormCommand(submitData, email));

            return Ok(pdfDocumentResponse);
        }

        /// <summary>
        /// Retrieve a credentialing form by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Successful request.</response>
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("{id}/credentialing-forms/{credId}")]
        public async Task<IActionResult> GetCredentialingForm([FromRoute] int credId)
        {
            var response = await _mediator.Send(new GetCredentialingFormStatusByIdQuery(credId));
            return Ok(response);
        }

        /// <summary>
        /// Return the latest credentialing form snapshot for a provider.
        /// </summary>
        /// <param name="id">The provider id</param>
        /// <returns></returns>
        /// <response code="200">Successful request.</response>
        [HttpGet("{id}/credentialing-forms/snapshot")]
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> GetLatestCredentialingFormSnapshot(int id)
        {
            return Ok(await _mediator.Send(new GetLatestCredentialingFormSnapshotQuery(id)));
        }

        /// <summary>
        /// Update a credentialing form snapshot and documents.
        /// </summary>
        /// <param name="fileDetail"></param>
        /// <param name="json"></param>
        /// <param name="providerId"></param>
        /// <param name="filesToDelete"></param>
        /// <returns></returns>
        [Authorize(Policy = CredPolicy.ACCESS_AS_PD)]
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPut("{id}/credentialing-forms/snapshot")]
        public async Task<IActionResult> MultiUploadDocument([FromForm] List<MultiFileUploadDto> fileDetail, [FromForm] string json,
            [FromForm] int providerId, [FromForm] string? filesToDelete)
        {
            //save Document and json
            await _mediator.Send(new ProcessDocumentJsonProviderCommand(fileDetail, json, providerId, filesToDelete));
            return Ok();
        }

        /// <summary>
        /// Return credentialing form status history for a provider.
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="limitPerPage"></param>
        /// <param name="id">The provider id</param>
        /// <returns></returns>
        /// <response code="200">Successful request.</response>
        [Authorize(Policy = CredPolicy.ACCESS_AS_PAD)]
        [HttpGet("{id}/credentialing-forms/statuses")]
        public async Task<IActionResult> GetInsurersByProvider
        (
            [FromQuery] int currentPage,
            [FromQuery] int limitPerPage,
            [FromRoute] int id
        )
        {
            var paginatedResponse = await _mediator.Send(new GetProviderInsurerStatusesQuery(id, currentPage, limitPerPage));
            return Ok(paginatedResponse);
        }

        #endregion

        #region delegates

        /// <summary>
        /// Create provider-delegate relation and invite the delegate to complete registration. If the delegate does not exist it will create a placeholder.
        /// </summary>
        /// <param name="id">The provider id</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("{id}/delegates")]
        public async Task<IActionResult> SendDelegateInvitationEmail([FromRoute] int id, [FromBody] DelegateInviteInfoDto request)
        {
            await _mediator.Send(new CreateProviderDelegateRelationCommand(ProviderId: id, DelegateEmail: request.Email, SendInvite: true));
            return Ok();
        }

        /// <summary>
        /// Retrieve all delegates for a provider.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="currentPage"></param>
        /// <param name="limitPerPage"></param>
        /// <returns></returns>
        /// <response code="200">Successful request.</response>
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("{id}/delegates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDelegates(int id, int currentPage, int limitPerPage)
        {
            return Ok(await _mediator.Send(new GetDelegatesByProviderIdQuery(id, currentPage, limitPerPage)));
        }

        #endregion

        #region documents

        /// <summary>
        /// Retrieve a list of documents for a provider.
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        [Authorize(Policy = CredPolicy.ACCESS_AS_ALL)]
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("{providerId}/credentialing-forms/documents")]
        public async Task<IActionResult> GetDocumentByProvider([FromRoute] int providerId)
        {
            _getB2CInfo.Role = User.FindFirst(CredTokenKey.ROLE)?.Value;
            _getB2CInfo.Email = User.FindFirst(CredTokenKey.EMAIL)?.Value;
            var response = await _documentDownloadService.GetAllDocumentByProviderId(providerId);
            return Ok(response);
        }

        /// <summary>
        /// Check if document was uploaded.
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        [Authorize(Policy = CredPolicy.ACCESS_AS_PD)]
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("{providerId}/credentialing-forms/documents/valid")]
        public IActionResult IsDocumentFound(DocumentReviewDto doc)
        {
            var isDocumentFound = _documentDownloadService.IsDocumentFound(doc);
            return Ok(isDocumentFound);
        }

        /// <summary>
        /// Download one or all documents for a provider.
        /// </summary>
        /// <param name="providerId"></param>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        //[Authorize(Policy = CredPolicy.ACCESS_AS_ALL)]
        //[AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [AllowAnonymous]
        [HttpPost("{providerId}/credentialing-forms/documents")]
        public async Task<IActionResult> GetDocument([FromRoute] int providerId, [FromBody] DocumentRequestDto requestDto)
        {
            _getB2CInfo.Role = User.FindFirst(CredTokenKey.ROLE)?.Value;
            _getB2CInfo.Email = User.FindFirst(CredTokenKey.EMAIL)?.Value;
            if ("ZIP".Equals(requestDto.Format))
            {
                if (requestDto.DownloadAll)
                {
                    DownloadDocumentsDto? file = await _documentDownloadService.DownloadDocumentZipByProviderIdAsync(providerId);
                    return File(file.Documents, file.ContentType, file.ZipFilename);
                }
                else
                {
                    List<DocumentSelectionDto> selection = new List<DocumentSelectionDto>();

                    DownloadDocumentsDto? file = await _documentDownloadService.DownloadDocumentZipByProviderSelectionAsync(requestDto.Filename);
                    return File(file.Documents, file.ContentType, file.ZipFilename);
                }
            }
            else if ("PDF".Equals(requestDto.Format))
            {
                if (requestDto.IsAzureBlobFilename)
                {
                    DownloadDocumentDto? file = await _documentDownloadService.DownloadDocumentAsync(requestDto.Filename[0]);
                    return File(file.Document, file.ContentType);
                }
                else
                {
                    var documentReview = new DocumentReviewDto()
                    {
                        DocumentTypeId = requestDto.DocumentTypeId,
                        ProviderId = providerId,
                        UploadFilename = requestDto.Filename[0]
                    };
                    DownloadDocumentDto? file = await _documentDownloadService.DownloadDocumentFromFormAsync(documentReview);
                    return File(file.Document, file.ContentType);
                }
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion
    }
}
