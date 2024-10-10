using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

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

        public object? Response { get; set; }

        [Required]
        public int StatusCode { get; set; }

    }
}
