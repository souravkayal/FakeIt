using FakeIt.Common.DTOs.ReadAPI;

namespace FakeIt.Service.ReadAPI
{
    public interface IReadAPIServiceInterface
    {
        Task<ReadAPIResponse> ReturnAPIResponse(ReadAPIRequest request);
    }
}
