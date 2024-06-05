using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.Mappers.DTOToEntity;
using cred_system_back_end_app.Application.UseCase.Insurer.DTO.RegisterProvider;
using cred_system_back_end_app.Application.UseCase.Insurer.DTO.ValidateAdmin;
using cred_system_back_end_app.Application.UseCase.OCSAdmin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cred_system_back_end_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OCSAdminController : ControllerBase
    {
        private readonly OCSAdminUseCase _ocsAdminUseCase;

        public OCSAdminController(OCSAdminUseCase ocsAdminUseCase)
        {
            _ocsAdminUseCase = ocsAdminUseCase;
        }

        [AllowAnonymous]
        [HttpPost("ValidateOCSAdmin")]
        public async Task<IActionResult> ValidateOCSAdmin(ValidateRequestDTO validateAdminRequestDTO)
        {
            var ocsAdminExists = await _ocsAdminUseCase.ValidateOCSAdmin(validateAdminRequestDTO.Email);

            var ocsAdminValidationResponse = GetAdminValidationResponse(ocsAdminExists);

            return Ok(ocsAdminValidationResponse);
        }

        [Authorize(Policy = CredPolicy.ACCESS_AS_ADMIN)]
        [HttpPut("UpdateOCSAdmin")]
        public async Task<IActionResult> UpdateInsurerAdmin([FromBody] UpdateOCSAdminRequestDTO updateOCSAdminRequestDTO)
        {
            await _ocsAdminUseCase.UpdateOCSAdmin(updateOCSAdminRequestDTO.GetNames(), updateOCSAdminRequestDTO.Email);

            return Ok();
        }

        #region helpers

        private static ValidationResponseDTO GetAdminValidationResponse(bool adminExists)
        {
            return new ValidationResponseDTO
            {
                Exists = adminExists,
            };
        }

        #endregion
    }
}
