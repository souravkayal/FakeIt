using Newtonsoft.Json;

namespace FakeIt.Common.Entity.CreateAPI
{
    public class CreateAPIRequest
    {
        public CreateAPIRequest() 
        {
            Id = Guid.NewGuid().ToString();
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("project_name")]
        public string ProjectName { get; set; } = string.Empty;

        [JsonProperty("url")]
        public string URL { get; set; } = string.Empty;

        [JsonProperty("response")]
        public string? Response { get; set; }
    }
}
