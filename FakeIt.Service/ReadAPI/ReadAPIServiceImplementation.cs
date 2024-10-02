using AutoMapper;
using Bogus;
using FakeIt.Common.DTOs.ReadAPI;
using FakeIt.Repository.ReadAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;

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
            // Use streaming parsing to reduce memory usage when parsing large JSON
            using var stringReader = new StringReader(sampleJson);
            using var jsonReader = new JsonTextReader(stringReader) { SupportMultipleContent = true };

            JToken sampleToken;
            if (!jsonReader.Read())
            {
                throw new ArgumentException("Invalid JSON provided.");
            }

            sampleToken = JToken.Load(jsonReader);

            // Ensure sampleJson is an array
            JArray sampleArray = sampleToken.Type == JTokenType.Array ? (JArray)sampleToken : new JArray(sampleToken);

            if (sampleArray.Count == 0)
            {
                throw new ArgumentException("Sample JSON must contain at least one object.");
            }

            // Use the first object as a template
            JObject sampleObject = (JObject)sampleArray[0];

            var fakeObjectsBag = new ConcurrentBag<JToken>();

            var faker = new Faker();

            Parallel.For(0, count, i =>
            {
                JObject fakeObject = GenerateFakeObject(sampleObject, faker);
                fakeObjectsBag.Add(fakeObject);
            });

            return fakeObjectsBag.ToList();
            
        }

        private static JObject GenerateFakeObject(JObject sampleObject, Faker faker)
        {
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
                        fakeValue = GenerateFakeObject((JObject)value, faker);
                        break;
                    case JTokenType.Array:
                        fakeValue = GenerateFakeArray((JArray)value, faker);
                        break;
                    default:
                        fakeValue = new JValue(faker.Lorem.Word());
                        break;
                }

                fakeObject.Add(property.Name, fakeValue);
            }

            return fakeObject;
        }

        private static JArray GenerateFakeArray(JArray sampleArray, Faker faker)
        {
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
                            fakeElement = GenerateFakeObject((JObject)sampleElement, faker);
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

                // Checking 404 is not possible, user may set 404 as response ;)
                if(response != null && (response.StatusCode != 404 && response.Message != "NoResultFound"))
                {
                    //If the request is to return the original response
                    if (request.Count == -1)
                    {
                        JToken token = JToken.Parse(response.Response.ToString());

                        if(token.Type == JTokenType.Array) 
                        {
                            List<JToken> jTokens = JsonConvert.DeserializeObject<List<JToken>>(response.Response.ToString());

                            return new ReadAPIResponse
                            {
                                StatusCode = response.StatusCode,
                                Response = jTokens.Select(jToken => jToken.ToObject<dynamic>()).ToList()
                            };

                        }
                        else
                        {
                            JToken jTokens = JsonConvert.DeserializeObject<JToken>(response.Response.ToString());

                            return new ReadAPIResponse
                            {
                                StatusCode = response.StatusCode,
                                Response = jTokens.ToObject<dynamic>()
                            };
                        }
                    }
                    else
                    {
                        //Make fake response for requested number of times
                        List<JToken> multipleObjectResponse = GenerateFakeObjects(response.Response.ToString(), request.Count);

                        return new ReadAPIResponse
                        {   
                            StatusCode = response.StatusCode,
                            Response = multipleObjectResponse.Select(jToken => jToken.ToObject<dynamic>()).ToList()
                        };
                    }
                   
                }

                return new ReadAPIResponse { StatusCode = response.StatusCode , Message = response.Message };

            }
            catch (AutoMapperMappingException ex)
            {
                throw new Exception("Mapping error occurred while creating static mapping.", ex);
            }
            
        }

    }
}
