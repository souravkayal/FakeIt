using FakeIt.Repository.CosmosConnector;
using Microsoft.Azure.Cosmos;

namespace FakeIt.Repository.ReadAPI
{
    public class ReadAPIRepositoryImplementation 
    {
        private readonly Container _container;

        public ReadAPIRepositoryImplementation(CosmosConnect cosmosConnect) 
        {
            //_container = cosmosConnect.GetContainer();
        }

        //
        //public async Task<CreateStaticResponse> CreateStaticMapping(CreateStaticRequest request)
        //{
        //    try
        //    {
        //        await _container.CreateItemAsync(request);

        //        return new CreateStaticResponse { Guid = Guid.NewGuid() };
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

    }
}
