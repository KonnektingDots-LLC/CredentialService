//using cred_system_back_end_app.Application.UseCase.Address;
//using cred_system_back_end_app.Application.UseCase.Address.DTO;
//using cred_system_back_end_app.Application.UseCase.Corporation;
//using cred_system_back_end_app.Application.UseCase.Corporation.DTO;
//using cred_system_back_end_app.Application.UseCase.Provider;
//using cred_system_back_end_app.Infrastructure.DB.Entity;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace cred_system_back_end_app.Controllers
//{
//    [Route("api")]
//    [ApiController]
//    public class CorporationController : ControllerBase
//    {
//        private readonly CorporationCase _corporationCase;

//        public CorporationController(CorporationCase corporationCase)
//        {

//            _corporationCase = corporationCase;

//        }

//        [HttpGet("[controller]/GetAll")]
//        public async Task<IActionResult> GetAsync()
//        {
//            var corporationDto = _corporationCase.GetAllCorporations();
//            return Ok(corporationDto);
//        }

//        [HttpGet("[controller]/{id}")]
//        [ProducesResponseType(typeof(CorporationEntity), StatusCodes.Status200OK)]
//        public IActionResult GetById(int id)
//        {
//            var corporationDto = _corporationCase.GetCorporationById(id);

//            return Ok(corporationDto);
//        }

//        [HttpPost("[controller]/Create")]
//        [ProducesResponseType(StatusCodes.Status201Created)]
//        public IActionResult Create(CreateCorporationDto createDto)
//        {

//            var newCorporation = _corporationCase.CreateCorporation(createDto);

//            return Ok(newCorporation);

//        }

//        [HttpPut("[controller]/Update")]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        public IActionResult Update(CorporationEntity corporationEntity)
//        {
//            var updateCorporation = _corporationCase.UpdateCorporation(corporationEntity);
//            return Ok(updateCorporation);
//        }

//    }
//}
