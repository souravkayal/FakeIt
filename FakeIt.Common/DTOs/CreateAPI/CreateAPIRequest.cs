﻿namespace FakeIt.Common.DTOs.CreateAPI
{
    public class CreateAPIRequest
    {
        public string ProjectName { get; set; } = string.Empty;
        public string URL { get; set; } = string.Empty;
        public string HttpMethod { get; set; } = string.Empty;
        public object? Response { get; set; }
        public int StatusCode { get; set; }
    }
}
