using FakeIt.Common.Entity.ReadAPI;

namespace FakeIt.Repository.ReadAPI
{
    public interface IReadAPIRepositoryInterface
    {
        Task<ReadAPIResponse> ReturnAPIResponse(ReadAPIRequest request);
    }
}
