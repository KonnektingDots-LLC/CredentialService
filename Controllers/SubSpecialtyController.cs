
using Microsoft.AspNetCore.Mvc;
using cred_system_back_end_app.Application.CRUD.SubSpecialty;
using Microsoft.Identity.Web;
using cred_system_back_end_app.Application.Common.Constants;

namespace cred_system_back_end_app.Controllers
{
    [Route("api")]
    [ApiController]
    public class SubSpecialtyController : ControllerBase
    {
        private readonly SubSpecialtyRepository _subspecialtyCase;

        public SubSpecialtyController(SubSpecialtyRepository subspecialtyCase)
        {

            _subspecialtyCase = subspecialtyCase;

        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("[controller]")]
        public IActionResult GetByOrganizationId(int organizationTypeId)
        {
            var specialtyDto = _subspecialtyCase.GetSubSpecialtyByOrganizationId(organizationTypeId);

            return Ok(specialtyDto);
        }
    }
}
