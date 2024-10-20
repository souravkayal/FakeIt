using System.Text.Json.Serialization;

namespace FakeIt.Common.APIModel.CreateAPI
{
    public class CreateAPIRequest
    {

        [JsonPropertyName("project-name")]
        public string ProjectName { get; set; }


        [JsonPropertyName("url")]
        public string URL { get; set; }


        [JsonPropertyName("http-method")]
        public string HttpMethod { get; set; }


        [JsonPropertyName("response")]
        public object? Response { get; set; }


        [JsonPropertyName("status-code")]
        public int StatusCode { get; set; }

    }
}
