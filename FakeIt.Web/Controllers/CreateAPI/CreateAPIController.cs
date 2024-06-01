using AutoMapper;
using FakeIt.Service.CreateAPI;
using Microsoft.AspNetCore.Mvc;

namespace FakeIt.Web.Controllers.CreateAPI
{
    [Route("api/create")]
    [ApiController]
    public class CreateAPIController : ControllerBase
    {
        private readonly ICreateAPIServiceInterface _createAPIServiceInterface;
        private readonly IMapper _mapper;

        public CreateAPIController(ICreateAPIServiceInterface createAPIServiceInterface 
            , IMapper mapper) 
        {
            _createAPIServiceInterface = createAPIServiceInterface;
            _mapper = mapper;
        }

        /// <summary>
        /// API to create static response
        /// </summary>
        /// <param name="createAPIRequest">Request payload</param>
        /// <returns>API create response</returns>
        [HttpPost("static-api-mapping")]
        public async Task<ActionResult<Common.APIModel.CreateAPI.CreateAPIResponse>> CreateStaticAPIMapping([FromBody] Common.APIModel.CreateAPI.CreateAPIRequest createAPIRequest)
        {
            var requestDto = _mapper.Map<Common.DTOs.CreateAPI.CreateAPIRequest>(createAPIRequest);
            
            var result = await _createAPIServiceInterface.CreateStaticMapping(requestDto);

            var responseResult =  _mapper.Map<Common.APIModel.CreateAPI.CreateAPIResponse>(result);

            return Ok(responseResult);
        }
    }
}
