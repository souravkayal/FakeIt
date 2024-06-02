using FakeIt.Common.Common;
using FakeIt.Common.Entity.ReadAPI;
using FakeIt.Repository.CosmosConnector;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FakeIt.Repository.ReadAPI
{
    public class ReadAPIRepositoryImplementation : IReadAPIRepositoryInterface
    {
        private readonly Container _container;

        public ReadAPIRepositoryImplementation(CosmosConnect cosmosConnect) 
        {
            _container = cosmosConnect.GetContainer(CosmosConstant.API_MASTER, CosmosConstant.API_MASTER_PARTITION_KEY);
        }

        private static string CleanJsonString(string jsonString)
        {
            // Check if the input is a JSON string with extra backslashes
            if (!string.IsNullOrEmpty(jsonString) && jsonString.StartsWith("\"") && jsonString.EndsWith("\""))
            {
                // Remove the surrounding double quotes
                jsonString = jsonString.Substring(1, jsonString.Length - 2);

                // Unescape the JSON string
                jsonString = jsonString.Replace("\\\"", "\"");

                // Parse the JSON string to ensure it is properly formatted
                var jToken = JToken.Parse(jsonString);

                // Convert back to JSON string without unnecessary backslashes
                return jToken.ToString(Formatting.None);
            }

            return jsonString;
        }

        public async Task<ReadAPIResponse> ReturnAPIResponse(ReadAPIRequest request)
        {
            try
            {
                var sqlQueryText = "SELECT c.response FROM c WHERE c.http_methode = @httpMethod AND c.url = @url";
                var queryDefinition = new QueryDefinition(sqlQueryText)
                    .WithParameter("@httpMethod", request.HttpMethod)
                    .WithParameter("@url", request.URL);

                var iterator = _container.GetItemQueryIterator<dynamic>(queryDefinition);

                if (iterator.HasMoreResults)
                {
                    FeedResponse<dynamic> response = await iterator.ReadNextAsync();
                    var document = response.FirstOrDefault();

                    if (document != null)
                    {
                        return new ReadAPIResponse
                        {
                            StatusCode = 200,
                            Response = System.Text.Json.JsonSerializer.Deserialize<dynamic>
                                       (JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(document.response)))
                        };
                    }
                }

                return new ReadAPIResponse
                {
                    StatusCode = 404,
                    Message = "No result found. Pls check the request URL."
                };
            }
            catch (CosmosException ex)
            {
                return new ReadAPIResponse
                {
                    StatusCode = (int) ex.StatusCode,
                    Message = ex.Message
                };
            }
            catch (Exception)
            {
                return new ReadAPIResponse
                {
                    StatusCode = 500,
                    Message = "Internal server error."
                };
            }
        }

    }
}
