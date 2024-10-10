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
        private readonly ILogger<CreateAPIController> _logger;

        public CreateAPIController(ICreateAPIServiceInterface createAPIServiceInterface 
            , IMapper mapper, 
            ILogger<CreateAPIController> logger) 
        {
            _createAPIServiceInterface = createAPIServiceInterface;
            _mapper = mapper;
            _logger = logger;
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
                    _logger.LogInformation($"Controller: Status code is not success");
                    return StatusCode(result.StatusCode , result.Message);
                }

                if (result == null)
                {
                    _logger.LogInformation($"Controller: Result is null from service");
                    return StatusCode(500, CommonConstants.INTERNAL_SERVER_ERROR);
                }

                var responseResult = _mapper.Map<Common.APIModel.CreateAPI.CreateAPIResponse>(result);

                return Ok(responseResult);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Controller: Auto mapper exception {ex.Message}");
                return StatusCode(500, $"Mapping error: {ex.Message}");
            }
            catch(Exception ex) 
            {
                _logger.LogError($"Controller: Exception {ex.Message}");
                return StatusCode(500, CommonConstants.INTERNAL_SERVER_ERROR);
            }
            
        }
    }
}
