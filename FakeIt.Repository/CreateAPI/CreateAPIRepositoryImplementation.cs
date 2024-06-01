using FakeIt.Common.Common;
using FakeIt.Common.Entity.CreateAPI;
using FakeIt.Repository.CosmosConnector;
using Microsoft.Azure.Cosmos;

namespace FakeIt.Repository.CreateAPI
{
    public class CreateAPIRepositoryImplementation : ICreateAPIRepositoryInterface
    {
        private readonly Container _container;

        public CreateAPIRepositoryImplementation(CosmosConnect cosmosConnect) 
        {
            _container = cosmosConnect.GetContainer(CosmosConstant.API_MASTER ,CosmosConstant.API_MASTER_PARTITION_KEY);
        }

        public async Task<CreateAPIResponse> CreateStaticMapping(CreateAPIRequest request)
        {
            try
            {
                await _container.CreateItemAsync(request);

                return new CreateAPIResponse { StatusCode = 200 , Message = $"Mock API successfully created for url - {request.URL}" };
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                return new CreateAPIResponse { StatusCode = 409, Message = "Url already exists in this project. Please use a different url." };
            }
            catch (CosmosException ex)
            {
                return new CreateAPIResponse { StatusCode = 500, Message = ex.Message };
            }
            catch (Exception)
            {
                return new CreateAPIResponse { Message = "Internal server error." };
            }
        }
    }
}
