using AutoMapper;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using cred_system_back_end_app.Application.CRUD.Insurer;
using cred_system_back_end_app.Application.CRUD.Insurer.DTO;

namespace cred_system_back_end_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsurerController_Old : ControllerBase
    {
        private readonly InsurerCase _insurerCase;

        public InsurerController_Old(InsurerCase insurerCase, IMapper mapper)
        {
            _insurerCase = insurerCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var insurersDto = _insurerCase.GetAllInsurers();
            return Ok(insurersDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(InsurerEntity), StatusCodes.Status200OK)]
        public IActionResult GetById(int id)
        {
            var insurerDto = _insurerCase.GetInsurerById(id);

            return Ok(insurerDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Create(CreateInsurerDto createDto)
        {

            var newInsurer = _insurerCase.CreateInsurer(createDto);

            return Ok(newInsurer);

        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(InsurerEntity insurerEntity)
        {
            var updateInsurer = _insurerCase.UpdateInsurer(insurerEntity);
            return Ok(updateInsurer);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            _insurerCase.DeleteInsurer(id);
            return NoContent();
        }

    }
}
