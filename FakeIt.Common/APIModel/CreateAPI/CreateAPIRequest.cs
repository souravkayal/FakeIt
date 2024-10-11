using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace FakeIt.Common.APIModel.CreateAPI
{
    public class CreateAPIRequest
    {
        public string ProjectName { get; set; } = string.Empty;

        public string URL { get; set; } = string.Empty;

        public string HttpMethod { get; set; } = string.Empty;

        public object? Response { get; set; }

        [Required]
        public int StatusCode { get; set; }

    }
}
