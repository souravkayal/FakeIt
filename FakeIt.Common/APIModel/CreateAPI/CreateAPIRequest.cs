using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FakeIt.Common.APIModel.CreateAPI
{
    public class CreateAPIRequest
    {
        
        [JsonPropertyName("project-name")]
        public string ProjectName { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string URL { get; set; } = string.Empty;

        [JsonPropertyName("http-method")]
        public string HttpMethod { get; set; } = string.Empty;

        [JsonPropertyName("response")]
        public object? Response { get; set; }

        [Required]
        [JsonPropertyName("status-code")]
        public int StatusCode { get; set; }

    }
}
