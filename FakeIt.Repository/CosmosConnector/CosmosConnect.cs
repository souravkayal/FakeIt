using Microsoft.Azure.Cosmos;
using System.Collections.ObjectModel;

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

        public async Task<Container> GetContainer(string containerName, string partitionKey)
        {
            // Ensure the database exists
            var databaseResponse = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseId);

            // Define partition key paths for hierarchical partition keys (e.g., /project_name, /url)
            ContainerProperties partitionKeyDefinition = new ContainerProperties
            {
                PartitionKeyPaths = new Collection<string>(new string[] { "/project_name", "/url" }) // Set multiple partition key paths
            };

            // Create container properties and set the PartitionKeyDefinition
            ContainerProperties containerProperties = new ContainerProperties(containerName, new string[] { "/project_name", "/url" });

            // Create the container if it doesn't exist, using the partition key definition
            var containerResponse = await databaseResponse.Database.CreateContainerIfNotExistsAsync(containerProperties);

            // Return the container
            return containerResponse.Container;
        }
    }
}
