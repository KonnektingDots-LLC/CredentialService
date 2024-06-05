using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.CRUD.Specialty;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using System.Net;

namespace cred_system_back_end_app.Controllers
{
    [Authorize]
    //[RequiredScope("access_as_user")]
    [Route("api")]
    [ApiController]
    public class SpecialtyController : ControllerBase
    {
        private readonly SpecialtyCase _specialtyCase;

        public SpecialtyController(SpecialtyCase specialtyCase)
        {

            _specialtyCase = specialtyCase;

        }

        [Authorize(Policy = CredPolicy.ACCESS_AS_PD)]
        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("[controller]")]
        [RequiredScope("read_data")]
        public IActionResult GetByOrganizationId(int organizationTypeId)
        {
            var specialtyDto = _specialtyCase.GetSpecialtyByOrganizationId(organizationTypeId);

            return Ok(specialtyDto);
        }

    }
}
