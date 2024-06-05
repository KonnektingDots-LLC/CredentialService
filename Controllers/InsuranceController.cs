using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.CRUD.Insurance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace cred_system_back_end_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceController : ControllerBase
    {
        private readonly InsuranceCase _insuranceCase;

        public InsuranceController(InsuranceCase insuranceCase)
        {
            _insuranceCase = insuranceCase;
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("MalpracticeCarrierList")]
        public IActionResult GetMalpracticeCarrierList()
        {
            var request = _insuranceCase.GetAllMalpracticeCarrier();
            return Ok(request);
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("ProfessionalLiabilityCarrierList")]
        public IActionResult GetProfessionalLiabilityCarrierList()
        {
            var request = _insuranceCase.GetAllProfessionalLiabilityCarrier();
            return Ok(request);
        }
    }
}
