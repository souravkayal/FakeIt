namespace FakeIt.Common.APIModel.ReadAPI
{
    public class ReadAPIRequest
    {
        public string URL { get; set; } = string.Empty;
        public string HttpMethod { get; set; } = string.Empty;
        public int Count {  get; set; }
        public int StatusCode { get; set; }
    }
}
