using AutoMapper;
using Azure;
using Azure.Communication.Email;
using cred_system_back_end_app.Application.Common.RequestDto;
using cred_system_back_end_app.Application.CRUD.Provider;
using cred_system_back_end_app.Application.CRUD.Provider.DTO;
using cred_system_back_end_app.Application.CRUD.ProviderDraft;
using cred_system_back_end_app.Application.CRUD.ProviderDraft.DTO;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using cred_system_back_end_app.Infrastructure.FileSystem.MultiFileUpload.DTO;
using cred_system_back_end_app.Infrastructure.FileSystem.MultiFileUpload;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cred_system_back_end_app.Application.UseCase.SaveJsonDraft;
using Microsoft.Identity.Web;
using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.Mappers.EntityToDTO;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Application.CRUD.Insurer;
using System.Reflection.Metadata.Ecma335;
using cred_system_back_end_app.Application.UseCase.Provider;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace cred_system_back_end_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {

        private readonly ProviderRepository _providerRepo;
        private readonly ProviderDraftCase _providerDraftCase;
        private readonly InsurerCompanyRepository _insurerCompanyRepo;
        private readonly ProviderUseCase _providerUseCase;
        private readonly ILogger<ProviderController> _logger;

        //private readonly SaveJsonDraftCase _saveJsonDraftCase;

        public ProviderController(
            ProviderRepository providerRepo,
            InsurerCompanyRepository insurerCompanyRepo,
            ProviderUseCase providerUseCase,
            ILogger<ProviderController> logger,
            IMapper mapper,
            ProviderDraftCase providerDraftCase)
        {
            // TODO: controller should not be using repos, only cases.

            _providerRepo = providerRepo;
            _providerDraftCase = providerDraftCase;
            _insurerCompanyRepo = insurerCompanyRepo;
            _providerUseCase = providerUseCase;
            _logger = logger;
            //_saveJsonDraftCase = saveJsonDraftCase;
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {            
            var providersDto = _providerRepo.GetAllProviders();
            return Ok(providersDto);
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("All")]
        public async Task<IActionResult> GetProviders([FromQuery] int currentPage, [FromQuery] int limitPerPage, string? search)
        {
            try
            {
                var role = User.FindFirst(CredTokenKey.ROLE)?.Value;
                var email = User.FindFirst(CredTokenKey.EMAIL)?.Value;

                var providers = await _providerUseCase.GetProvidersByRole(role, email, currentPage, limitPerPage, search);              

                return Ok(providers);

            }catch(Exception ex) 
            {
                _logger.LogInformation(ex.Message);
                throw ex;
            };
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProviderEntity), StatusCodes.Status200OK)]
        public IActionResult GetById(int id)
        {
            var providerDto = _providerRepo.GetProviderById(id);
    
            return Ok(providerDto);
        }

        //[AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //public IActionResult Create(CreateProviderDto createDto)
        //{
        //    _providerRepo.SetEmail(User.FindFirst("emails")?.Value);
        //    var newProvider = _providerRepo.CreateProvider(createDto);

        //    return Ok(newProvider);

        //}

        [AuthorizeForScopes (Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(ProviderEntity providerEntity)
        { 
            var updateProvider = _providerRepo.UpdateProvider(providerEntity);
            return Ok(updateProvider);
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {            
            _providerRepo.DeleteProvider(id);
            return NoContent();
        }


        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("SaveJsonProviderDraft")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> SaveJsonProviderDraft(ProviderDraftDto request)
        {

            var newProvider = await _providerDraftCase.SaveJsonProvider(request);

            return Ok(newProvider);

        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("GetJsonProviderDraft")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> GetJsonProviderDraft(int providerId)
        {

            var newProvider = await _providerDraftCase.GetJsonProvider(providerId);

            return Ok(newProvider);

        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("ProviderType")]
        public IActionResult GetProviderTypeAsync()
        {
            var providerTypes = _providerRepo.GetAllProviderTypes();
            return Ok(providerTypes);
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("AcceptPlanList")]
        public IActionResult GetAcceptPlanListAsync()
        {
            var providerTypes = _providerRepo.GetAllAcceptPlanList();
            return Ok(providerTypes);
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("ByEmail")]
        public IActionResult GetProviderByEmail(string email)
        {
            var provider = _providerRepo.GetProviderByEmail(email);
            return Ok(provider);
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("ByCredFormId")]
        public IActionResult GetProviderByCredFormId(int credFormId)
        {
            var provider = _providerRepo.GetProviderByCredFormId(credFormId);
            return Ok(provider);
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("IsSubmitted")]
        public async Task<IActionResult> IsSubmitted(int providerId)
        {
            var IsSubmitted = await _providerDraftCase.IsProviderCompleted(providerId);
            return Ok(IsSubmitted);
        }

        //Test
        [AllowAnonymous]
        [HttpPost("SaveJson")]
        public async Task<IActionResult> SaveJson(string json, int providerId, string modifiedBy)
        {
            await _providerDraftCase.ModifyJsonProvider(json, providerId, modifiedBy);
            return Ok();
        }


    }
}
