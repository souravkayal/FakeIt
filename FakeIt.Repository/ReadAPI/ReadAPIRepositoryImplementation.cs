﻿using FakeIt.Common.Common;
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

        public async Task<ReadAPIResponse> ReturnAPIResponse(ReadAPIRequest request)
        {
            try
            {
                var sqlQueryText = "SELECT c.status_code, c.response FROM c WHERE c.http_methode = @httpMethod AND c.url = @url";

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
                            StatusCode = document.status_code,
                            Response = document.response
                        };
                    }
                }

                return new ReadAPIResponse
                {
                    StatusCode = 404,
                    Message = "NoResultFound"
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
                    Message = CommonConstants.INTERNAL_SERVER_ERROR
                };
            }
        }

    }
}
