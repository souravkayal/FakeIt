using FakeIt.Common.DTOs.CreateAPI;
using FakeIt.Repository.CreateAPI;

namespace FakeIt.Service.CreateAPI
{
    public class CreateAPIServiceImplementation : ICreateAPIServiceInterface
    {
        private readonly ICreateAPIRepositoryInterface _createAPIRepositoryInterface;

        public CreateAPIServiceImplementation(ICreateAPIRepositoryInterface createAPIRepositoryInterface) 
        {
            this._createAPIRepositoryInterface = createAPIRepositoryInterface;
        }

        public async Task<CreateStaticResponse> CreateStaticMapping(CreateStaticRequest request)
        {
            var requestEntity = new FakeIt.Common.Entity.CreateAPI.CreateStaticRequest
            {
                Response = request.Response,
                URL = request.URL,
            };

            var result = await _createAPIRepositoryInterface.CreateStaticMapping(requestEntity);

            return new CreateStaticResponse 
            { 
                Guid = Guid.NewGuid(),
            };
        }
    }
}
