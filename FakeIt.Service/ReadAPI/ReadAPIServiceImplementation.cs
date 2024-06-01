using AutoMapper;
using FakeIt.Common.DTOs.ReadAPI;
using FakeIt.Repository.ReadAPI;

namespace FakeIt.Service.ReadAPI
{
    public class ReadAPIServiceImplementation : IReadAPIServiceInterface
    {

        private readonly IReadAPIRepositoryInterface _readAPIRepositoryInterface;
        private readonly IMapper _mapper;

        public ReadAPIServiceImplementation(IReadAPIRepositoryInterface readAPIRepositoryInterface, 
            IMapper mapper) 
        {
            _readAPIRepositoryInterface = readAPIRepositoryInterface;
            _mapper = mapper;
        }

        public async Task<ReadAPIResponse> ReturnAPIResponse(ReadAPIRequest request)
        {
            var requestEnt = _mapper.Map<Common.Entity.ReadAPI.ReadAPIRequest>(request);

            var response = await _readAPIRepositoryInterface.ReturnAPIResponse(requestEnt);

            return _mapper.Map<ReadAPIResponse>(response);
        }

    }
}
