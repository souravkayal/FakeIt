using FakeIt.Common.Common;
using Newtonsoft.Json.Linq;

namespace FakeIt.Common.APIModel.ReadAPI
{
    public class ReadAPIResponse : ResponseBase
    {
        public dynamic Response { get; set; }
    }
}
