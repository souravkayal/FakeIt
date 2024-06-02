using AutoMapper;
using FakeIt.Common.APIModel.ReadAPI;
using FakeIt.Service.ReadAPI;
using Microsoft.AspNetCore.Mvc;

namespace FakeIt.Web.Controllers.CreateAPI
{
    [ApiController]
    [Route("{*url}")]
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
        public async Task<ReadAPIResponse> HandleAPIReadRequest()
        {
            try
            {
                var endpoint = $"/{Request?.Path.Value?.TrimStart('/')}";

                if (string.IsNullOrEmpty(endpoint))
                {
                    return new ReadAPIResponse { StatusCode = 400, Message = "Invalid URL error : Pls provide full URL." };
                }

                var readRequestDto = _mapper.Map<Common.DTOs.ReadAPI.ReadAPIRequest>(new ReadAPIRequest
                {
                    HttpMethod = Request?.Method,
                    URL = endpoint
                });

                var result = await _readAPIServiceInterface.ReturnAPIResponse(readRequestDto);

                return _mapper.Map<ReadAPIResponse>(result);
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
