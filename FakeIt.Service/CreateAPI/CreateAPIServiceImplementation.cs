using AutoMapper;
using FakeIt.Common.DTOs.CreateAPI;
using FakeIt.Repository.CreateAPI;

namespace FakeIt.Service.CreateAPI
{
    public class CreateAPIServiceImplementation : ICreateAPIServiceInterface
    {
        private readonly ICreateAPIRepositoryInterface _createAPIRepositoryInterface;
        private readonly IMapper _mapper;

        public CreateAPIServiceImplementation(
            ICreateAPIRepositoryInterface createAPIRepositoryInterface,
            IMapper mapper) 
        {
            _createAPIRepositoryInterface = createAPIRepositoryInterface;
            _mapper = mapper;
        }

        public async Task<CreateAPIResponse> CreateStaticMapping(CreateAPIRequest request)
        {
            var requestEntity = _mapper.Map<FakeIt.Common.Entity.CreateAPI.CreateAPIRequest>(request);
            
            var result = await _createAPIRepositoryInterface.CreateStaticMapping(requestEntity);
            
            return _mapper.Map<CreateAPIResponse>(result);
        }
    }
}
