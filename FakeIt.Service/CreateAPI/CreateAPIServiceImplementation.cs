using AutoMapper;
using FakeIt.Common.DTOs.CreateAPI;
using FakeIt.Repository.CreateAPI;
using Microsoft.Extensions.Logging;

namespace FakeIt.Service.CreateAPI
{
    public class CreateAPIServiceImplementation : ICreateAPIServiceInterface
    {
        private readonly ICreateAPIRepositoryInterface _createAPIRepositoryInterface;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAPIServiceImplementation> _logger;

        public CreateAPIServiceImplementation(
            ICreateAPIRepositoryInterface createAPIRepositoryInterface,
            IMapper mapper,
            ILogger<CreateAPIServiceImplementation> logger)
        {
            _createAPIRepositoryInterface = createAPIRepositoryInterface;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CreateAPIResponse> CreateStaticMapping(CreateAPIRequest request)
        {
            try
            {
                var requestEntity = _mapper.Map<FakeIt.Common.Entity.CreateAPI.CreateAPIRequest>(request);

                var result = await _createAPIRepositoryInterface.CreateStaticMapping(requestEntity);

                _logger.LogError($"Service: create completed");

                return _mapper.Map<CreateAPIResponse>(result);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Service: Exception in auto mapping {ex.Message}");
                throw new Exception("Mapping error occurred while creating static mapping.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Service: Exception in service {ex.Message}");
                throw;
            }

        }
    }
}
