using FakeIt.Common.Common;
using FakeIt.Common.Entity.CreateAPI;
using FakeIt.Repository.CosmosConnector;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace FakeIt.Repository.CreateAPI
{
    public class CreateAPIRepositoryImplementation : ICreateAPIRepositoryInterface
    {
        private readonly Container _container;
        private readonly ILogger<CreateAPIRepositoryImplementation> _logger;

        public CreateAPIRepositoryImplementation(CosmosConnect cosmosConnect, 
            ILogger<CreateAPIRepositoryImplementation> logger) 
        {
            _container = cosmosConnect.GetContainer(CosmosConstant.API_MASTER ,CosmosConstant.API_MASTER_PARTITION_KEY).Result;
            _logger = logger;
        }

        public async Task<CreateAPIResponse> CreateStaticMapping(CreateAPIRequest request)
        {
            try
            {

                await _container.CreateItemAsync(request);

                _logger.LogInformation($"Repository: document created successfully.");

                return new CreateAPIResponse 
                { 
                    StatusCode = 200 , 
                    Message = $"Mock API successfully created for url - {request.URL}" 
                };
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                _logger.LogError($"Repository: Information already exists in db {ex.Message}.");

                return new CreateAPIResponse 
                { 
                    StatusCode = 409, 
                    Message = "Url already exists in this project. Please use a different url." 
                };
            }
            catch (CosmosException ex)
            {
                _logger.LogError($"Repository: Exception in cosmos db {ex.Message}.");

                return new CreateAPIResponse 
                { 
                    StatusCode = 500, 
                    Message = ex.Message 
                };
            }
            catch(Exception ex)
            {
                _logger.LogError($"Repository: Exception {ex.Message}.");

                return new CreateAPIResponse 
                { 
                    StatusCode = 500,
                    Message = CommonConstants.INTERNAL_SERVER_ERROR
                };
            }
        }
    }
}
