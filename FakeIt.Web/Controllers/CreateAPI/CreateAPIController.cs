using FakeIt.Common.APIModel.CreateAPI;
using FakeIt.Common.DTOs.CreateAPI;
using FakeIt.Service.CreateAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FakeIt.Web.Controllers.CreateAPI
{
    [Route("api/create")]
    [ApiController]
    public class CreateAPIController : ControllerBase
    {
        private readonly ICreateAPIServiceInterface _createAPIServiceInterface;

        public CreateAPIController(ICreateAPIServiceInterface _createAPIServiceInterface) 
        {
            this._createAPIServiceInterface = _createAPIServiceInterface;
        }

        [HttpPost("static-api-mapping")]
        public async Task<ActionResult<CreateStaticAPIResponse>> CreateStaticAPIMapping([FromBody] CreateStaticAPIRequest createAPIRequest)
        {

            var request = new CreateStaticRequest 
            { 
                URL = createAPIRequest.URL , 
                Response = createAPIRequest.Response 
            };
            
            var result = await _createAPIServiceInterface.CreateStaticMapping(request);

            var response = new CreateStaticAPIResponse
            {
               Guid = result.Guid
            };

            return Ok(response);
        }

    }
}
