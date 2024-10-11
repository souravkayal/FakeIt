namespace FakeIt.Common.Entity.ReadAPI
{
    public class ReadAPIRequest
    {
        public string ProjectName { get; set; }
        public string URL { get; set; } = string.Empty;
        public string HttpMethod { get; set; } = string.Empty;
    }
}
