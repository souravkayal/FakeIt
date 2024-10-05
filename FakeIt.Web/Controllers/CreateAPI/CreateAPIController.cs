using AutoMapper;
using FakeIt.Common.Common;
using FakeIt.Service.CreateAPI;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FakeIt.Web.Controllers.CreateAPI
{

    [Route("api/")]
    [ApiController]
    public class CreateAPIController : ControllerBase
    {
        private readonly ICreateAPIServiceInterface _createAPIServiceInterface;
        private readonly IMapper _mapper;
        //TODO: add logger

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
        [HttpPost("create")]
        public async Task<ActionResult<Common.APIModel.CreateAPI.CreateAPIResponse>> CreateStaticAPIMapping([FromBody] Common.APIModel.CreateAPI.CreateAPIRequest createAPIRequest)
        {
            
            try
            {
                if (createAPIRequest == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "Please provide valid request setting");
                }

                if ( createAPIRequest.StatusCode == 0 ) 
                {
                    return StatusCode((int) HttpStatusCode.BadRequest, "Please provide valid HTTP status code in request.");
                }

                if (String.IsNullOrEmpty(createAPIRequest.ProjectName))
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "Please provide project name in request.");
                }

                if (!CommonHelper.IsValidHttpMethod(createAPIRequest.HttpMethod))
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "Please provide valid HTTP method name in request.");
                }

                if(createAPIRequest.Response == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "Please provide response message.");
                }

                //TODO: validate response as valid JSON


                var requestDto = _mapper.Map<Common.DTOs.CreateAPI.CreateAPIRequest>(createAPIRequest);

                var result = await _createAPIServiceInterface.CreateStaticMapping(requestDto);

                if(result.StatusCode != 200)
                {
                    return StatusCode(result.StatusCode , result.Message);
                }

                if (result == null)
                {
                    return StatusCode(500, CommonConstants.INTERNAL_SERVER_ERROR);
                }

                var responseResult = _mapper.Map<Common.APIModel.CreateAPI.CreateAPIResponse>(result);

                return Ok(responseResult);
            }
            catch (AutoMapperMappingException ex)
            {
                return StatusCode(500, $"Mapping error: {ex.Message}");
            }
            catch 
            {
                return StatusCode(500, CommonConstants.INTERNAL_SERVER_ERROR);
            }
            
        }
    }
}
