using AutoMapper;
using cred_system_back_end_app.Application.Common.RequestDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static cred_system_back_end_app.Application.Common.RequestDto.CreateAccountDto;

namespace cred_system_back_end_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IMapper _mapper;

        public UserController(IMapper mapper)
        {
            _mapper = mapper;
        }

    }
}
