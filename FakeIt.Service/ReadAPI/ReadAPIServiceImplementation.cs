using AutoMapper;
using Bogus;
using FakeIt.Common.DTOs.ReadAPI;
using FakeIt.Repository.ReadAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        #region Private methods to generate fake data

        private static List<JToken> GenerateFakeObjects(string sampleJson, int count)
        {
            // Deserialize the sample JSON to get the structure
            JArray sampleArray = JArray.Parse(sampleJson);

            if (sampleArray.Count == 0)
            {
                throw new ArgumentException("Sample JSON array must contain at least one object.");
            }

            // Use the first object as a template
            JObject sampleObject = (JObject)sampleArray[0];

            List<JToken> fakeObjects = new List<JToken>();

            for (int i = 0; i < count; i++)
            {
                JObject fakeObject = GenerateFakeObject(sampleObject);
                fakeObjects.Add(fakeObject);
            }

            return fakeObjects;
        }

        private static JObject GenerateFakeObject(JObject sampleObject)
        {
            var faker = new Faker();
            JObject fakeObject = new JObject();

            foreach (var property in sampleObject.Properties())
            {
                JToken value = property.Value;
                JToken fakeValue;

                switch (value.Type)
                {
                    case JTokenType.Integer:
                        fakeValue = new JValue(faker.Random.Int(1, 1000));
                        break;
                    case JTokenType.String:
                        fakeValue = value.ToString().Contains("@") ? new JValue(faker.Internet.Email()) : new JValue(faker.Name.FullName());
                        break;
                    case JTokenType.Float:
                        fakeValue = new JValue(faker.Random.Double(1.0, 1000.0));
                        break;
                    case JTokenType.Boolean:
                        fakeValue = new JValue(faker.Random.Bool());
                        break;
                    case JTokenType.Date:
                        fakeValue = new JValue(faker.Date.Past());
                        break;
                    case JTokenType.Guid:
                        fakeValue = new JValue(faker.Random.Guid());
                        break;
                    case JTokenType.Object:
                        fakeValue = GenerateFakeObject((JObject)value);
                        break;
                    case JTokenType.Array:
                        fakeValue = GenerateFakeArray((JArray)value);
                        break;
                    default:
                        fakeValue = new JValue(faker.Lorem.Word());
                        break;
                }

                fakeObject.Add(property.Name, fakeValue);
            }

            return fakeObject;
        }

        private static JArray GenerateFakeArray(JArray sampleArray)
        {
            var faker = new Faker();
            JArray fakeArray = new JArray();

            if (sampleArray.Count > 0)
            {
                var sampleElement = sampleArray[0];

                for (int i = 0; i < 5; i++)
                {
                    JToken fakeElement;
                    switch (sampleElement.Type)
                    {
                        case JTokenType.Object:
                            fakeElement = GenerateFakeObject((JObject)sampleElement);
                            break;
                        case JTokenType.Integer:
                            fakeElement = new JValue(faker.Random.Int(1, 1000));
                            break;
                        case JTokenType.String:
                            fakeElement = new JValue(faker.Lorem.Word());
                            break;
                        case JTokenType.Float:
                            fakeElement = new JValue(faker.Random.Double(1.0, 1000.0));
                            break;
                        case JTokenType.Boolean:
                            fakeElement = new JValue(faker.Random.Bool());
                            break;
                        case JTokenType.Date:
                            fakeElement = new JValue(faker.Date.Past());
                            break;
                        case JTokenType.Guid:
                            fakeElement = new JValue(faker.Random.Guid());
                            break;
                        default:
                            fakeElement = new JValue(faker.Lorem.Word());
                            break;
                    }
                    fakeArray.Add(fakeElement);
                }
            }

            return fakeArray;
        }

        #endregion

        public async Task<ReadAPIResponse> ReturnAPIResponse(ReadAPIRequest request)
        {
            try
            {
                var requestEnt = _mapper.Map<Common.Entity.ReadAPI.ReadAPIRequest>(request);

                var response = await _readAPIRepositoryInterface.ReturnAPIResponse(requestEnt);

                //If the request is to return the original response
                if(request.Count == -1) 
                {
                    List<JToken> jTokens = JsonConvert.DeserializeObject<List<JToken>>(response.Response.ToString());

                    return new ReadAPIResponse 
                    {
                        Response = jTokens.Select(jToken => jToken.ToObject<dynamic>()).ToList() 
                    };
                }

                //Make fake response for requested number of times
                List<JToken> multipleObjectResponse = GenerateFakeObjects(response.Response.ToString(), request.Count);

                return new ReadAPIResponse 
                {  
                    Response = multipleObjectResponse.Select(jToken => jToken.ToObject<dynamic>()).ToList() 
                };

            }
            catch (AutoMapperMappingException ex)
            {
                throw new Exception("Mapping error occurred while creating static mapping.", ex);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

    }
}
