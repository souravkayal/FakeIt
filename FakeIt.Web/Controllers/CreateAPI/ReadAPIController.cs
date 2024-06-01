using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace FakeIt.Web.Controllers.CreateAPI
{
    [Route("api/read")]
    [ApiController]
    public class ReadAPIController : ControllerBase
    {
        private readonly IMapper _mapper;

        public ReadAPIController(IMapper mapper) 
        {
            _mapper = mapper;
        }




    }
}
