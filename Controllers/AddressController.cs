using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using cred_system_back_end_app.Application.CRUD.Address;
using Microsoft.Identity.Web;
using cred_system_back_end_app.Application.Common.Constants;

namespace cred_system_back_end_app.Controllers
{
    [Route("api")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AddressCase _addressCase;

        public AddressController(AddressCase addressCase)
        {

            _addressCase = addressCase;

        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("[controller]/State")]
        public async Task<IActionResult> GetStateAsync()
        {
            var addresssDto = _addressCase.GetAllAddressState();
            return Ok(addresssDto);
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("[controller]/Type")]
        public async Task<IActionResult> GetTypeAsync()
        {
            var addresssDto = _addressCase.GetAllAddressType();
            return Ok(addresssDto);
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("[controller]/Country")]
        public async Task<IActionResult> GetCountryAsync()
        {
            var addresssDto = _addressCase.GetAllAddressCountry();
            return Ok(addresssDto);
        }

    }
}
