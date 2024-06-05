using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.CRUD.Hospital;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace cred_system_back_end_app.Controllers
{
    [Route("api")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly HospitalCase _hospitalCase;

        public HospitalController(HospitalCase HospitalCase)
        {

            _hospitalCase = HospitalCase;

        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("[controller]/List")]
        public IActionResult GetHospitals()
        {
            var HospitalDto = _hospitalCase.GetAllHospitalList();
            return Ok(HospitalDto);
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpGet("[controller]/PrivilegesType")]
        public IActionResult GetPrivileges()
        {
            var HospitalDto = _hospitalCase.GetAllPrivilegeList();
            return Ok(HospitalDto);
        }

        //    [HttpGet("[controller]/GetAll")]
        //    public async Task<IActionResult> GetAsync()
        //    {
        //        var HospitalDto = _hospitalCase.GetAllHospital();
        //        return Ok(HospitalDto);
        //    }

        //    [HttpGet("[controller]/{id}")]
        //    [ProducesResponseType(typeof(PSubSpecialtyEntity), StatusCodes.Status200OK)]
        //    public IActionResult GetById(int id)
        //    {
        //        var HospitalDto = _hospitalCase.GetHospitalById(id);

        //        return Ok(HospitalDto);
        //    }

        //    [HttpPost("[controller]/Create")]
        //    [ProducesResponseType(StatusCodes.Status201Created)]
        //    public IActionResult Create(CreateHospitalDto createDto)
        //    {

        //        var newHospital = _hospitalCase.CreateHospital(createDto);

        //        return Ok(newHospital);

        //    }

        //    [HttpPut("[controller]/Update")]
        //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //    public IActionResult Update(HospitalEntity hospitalEntity)
        //    {
        //        var updateHospital = _hospitalCase.UpdateHospital(hospitalEntity);
        //        return Ok(updateHospital);
        //    }
    }
}
