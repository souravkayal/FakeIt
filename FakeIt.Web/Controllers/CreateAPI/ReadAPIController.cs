using AutoMapper;
using FakeIt.Common.APIModel.ReadAPI;
using FakeIt.Common.Common;
using FakeIt.Service.ReadAPI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FakeIt.Web.Controllers.CreateAPI
{
    [ApiController]
    [Route("/read/{*url}")]
    public class ReadAPIController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReadAPIServiceInterface _readAPIServiceInterface;

        public ReadAPIController(IMapper mapper, 
            IReadAPIServiceInterface readAPIServiceInterface) 
        {
            _mapper = mapper;
            _readAPIServiceInterface = readAPIServiceInterface;
        }

        [HttpGet, HttpPost]
        public async Task<ReadAPIResponse> HandleAPIReadRequest([FromQuery] int count = -1)
        {
            try
            {
                var endpoint = CommonHelper.GetPartAfterRead($"/{Request?.Path.Value?.TrimStart('/')}");
                
                if (string.IsNullOrEmpty(endpoint))
                {
                    return new ReadAPIResponse { StatusCode = 400, Message = "Invalid URL error : Pls provide full URL." };
                }

                if (count < -1 || count == 0 || count > 100)
                {
                    return new ReadAPIResponse { StatusCode = 400, Message = "Invalid count. Count must be in range of 1 to 100. Ignore count to retuen the origianl document" };
                }

                var readRequestDto = _mapper.Map<Common.DTOs.ReadAPI.ReadAPIRequest>(new ReadAPIRequest
                {
                    HttpMethod = Request?.Method,
                    URL = endpoint,
                    Count = count
                });

                var result = await _readAPIServiceInterface.ReturnAPIResponse(readRequestDto);

                if(result.StatusCode == 200) 
                {
                    return new ReadAPIResponse
                    {
                        Response = System.Text.Json.JsonSerializer.Deserialize<dynamic>(JsonConvert.SerializeObject(result.Response))
                    };
                }

                return new ReadAPIResponse { StatusCode = result.StatusCode, Message = result.Message };
            }
            catch (AutoMapperMappingException ex)
            {
                return new ReadAPIResponse { StatusCode = 500, Message = $"Mapping error: {ex.Message}" };
            }
            catch (Exception ex)
            {
                return new ReadAPIResponse { StatusCode = 500, Message = CommonConstants.INTERNAL_SERVER_ERROR };
            }

        }

    }
}
