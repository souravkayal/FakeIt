using System.ComponentModel.DataAnnotations;

namespace FakeIt.Common.APIModel.CreateAPI
{
    public class CreateAPIRequest
    {

        [Required(ErrorMessage = "ProjectName is required.")]
        public string ProjectName { get; set; } = string.Empty;


        [Required(ErrorMessage = "URL is required.")]
        public string URL { get; set; } = string.Empty;


        [Required(ErrorMessage = "HTTP method is requirred.")]
        [AllowedValues("GET", "POST", ErrorMessage = "GET and POST are allowed in HTTP method")]
        public string HttpMethod { get; set; } = string.Empty;


        [Required(ErrorMessage = "Response is required.")]
        public dynamic Response { get; set; }

    }
}
