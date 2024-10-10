using FakeIt.Common.Common;
using Newtonsoft.Json.Linq;

namespace FakeIt.Common.DTOs.ReadAPI
{
    public class ReadAPIResponse : ResponseBase
    {
        public object? Response { get; set; }
    }
}
