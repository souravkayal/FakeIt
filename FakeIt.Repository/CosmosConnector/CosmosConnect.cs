using Microsoft.Azure.Cosmos;

namespace FakeIt.Repository.CosmosConnector
{
    public class CosmosConnect
    {
        private CosmosClient _cosmosClient;
        private string _databaseId;

        public CosmosConnect(CosmosClient cosmosClient, string databaseId)
        {
            _cosmosClient = cosmosClient;
            _databaseId = databaseId;
        }

        public Container GetContainer(string containerName, string partitionKey)
        {
            var database = _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseId).Result;
            return database.Database.CreateContainerIfNotExistsAsync(containerName, $"/{partitionKey}").Result.Container;
        }
    }
}
