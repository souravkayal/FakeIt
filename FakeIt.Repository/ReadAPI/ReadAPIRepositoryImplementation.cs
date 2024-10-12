using FakeIt.Common.Common;
using FakeIt.Common.Entity.ReadAPI;
using FakeIt.Repository.CosmosConnector;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FakeIt.Repository.ReadAPI
{
    public class ReadAPIRepositoryImplementation : IReadAPIRepositoryInterface
    {
        private readonly Container _container;
        private readonly ILogger<ReadAPIRepositoryImplementation> _logger;

        public ReadAPIRepositoryImplementation(CosmosConnect cosmosConnect , 
            ILogger<ReadAPIRepositoryImplementation> logger) 
        {
            _container = cosmosConnect.GetContainer(CosmosConstant.API_MASTER, CosmosConstant.API_MASTER_PARTITION_KEY).Result;
            _logger = logger;
        }

        public async Task<ReadAPIResponse> ReturnAPIResponse(ReadAPIRequest request)
        {
            try
            {
                var sqlQueryText = "SELECT c.status_code, c.response FROM c WHERE c.http_methode = @httpMethod AND c.url = @url AND c.project_name=@projectName";

                var queryDefinition = new QueryDefinition(sqlQueryText)
                    .WithParameter("@httpMethod", request.HttpMethod)
                    .WithParameter("@url", request.URL)
                    .WithParameter("@projectName", request.ProjectName);

                _logger.LogInformation($"Query- {queryDefinition.QueryText} - {request.HttpMethod} - {request.URL} - {request.ProjectName}");

                var iterator = _container.GetItemQueryIterator<dynamic>(queryDefinition);
                
                if (iterator.HasMoreResults)
                {
                    _logger.LogInformation($"Repository: Result found in db");

                    FeedResponse<dynamic> response = await iterator.ReadNextAsync();
                    var document = response.FirstOrDefault();

                    if (document != null)
                    {
                        _logger.LogInformation($"Repository: Document found in db");

                        return new ReadAPIResponse
                        {
                            StatusCode = document.status_code,
                            Response = document.response
                        };
                    }
                    else
                        _logger.LogInformation($"Repository: Document is null");
                }

                _logger.LogInformation($"Repository: Result not found in db");

                return new ReadAPIResponse
                {
                    StatusCode = 404,
                    Message = "NoResultFound"
                };
            }
            catch (CosmosException ex)
            {
                _logger.LogInformation($"Repository: Cosmos exception {ex.Message}");

                return new ReadAPIResponse
                {
                    StatusCode = (int) ex.StatusCode,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Repository: Exception {ex.Message}");

                return new ReadAPIResponse
                {
                    StatusCode = 500,
                    Message = CommonConstants.INTERNAL_SERVER_ERROR
                };
            }
        }

    }
}
