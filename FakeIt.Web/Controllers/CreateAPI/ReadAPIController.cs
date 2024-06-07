using AutoMapper;
using FakeIt.Common.APIModel.ReadAPI;
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
        public static string GetPartAfterRead(string input)
        {
            string keyword = "/read";
            int index = input.IndexOf(keyword);

            if (index == -1)
            {
                return string.Empty; // or throw an exception if you prefer
            }

            return input.Substring(index + keyword.Length);
        }

        [HttpGet, HttpPost]
        public async Task<ReadAPIResponse> HandleAPIReadRequest([FromQuery] int count = -1)
        {
            try
            {
                var endpoint = GetPartAfterRead($"/{Request?.Path.Value?.TrimStart('/')}");
                
                if (string.IsNullOrEmpty(endpoint))
                {
                    return new ReadAPIResponse { StatusCode = 400, Message = "Invalid URL error : Pls provide full URL." };
                }

                var readRequestDto = _mapper.Map<Common.DTOs.ReadAPI.ReadAPIRequest>(new ReadAPIRequest
                {
                    HttpMethod = Request?.Method,
                    URL = endpoint,
                    Count = count
                });

                var result = await _readAPIServiceInterface.ReturnAPIResponse(readRequestDto);

                return new ReadAPIResponse 
                { 
                    Response = System.Text.Json.JsonSerializer.Deserialize<dynamic>(JsonConvert.SerializeObject(result.Response)) 
                };

            }
            catch (AutoMapperMappingException ex)
            {
                return new ReadAPIResponse { StatusCode = 500, Message = $"Mapping error: {ex.Message}" };
            }
            catch (Exception ex)
            {
                return new ReadAPIResponse { StatusCode = 500, Message = $"An unexpected error occurred: {ex.Message}" };
            }

        }

    }
}
