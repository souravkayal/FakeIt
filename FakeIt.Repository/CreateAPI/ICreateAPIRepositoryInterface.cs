using FakeIt.Common.Entity.CreateAPI;

namespace FakeIt.Repository.CreateAPI
{
    public interface ICreateAPIRepositoryInterface
    {
        Task<CreateAPIResponse> CreateStaticMapping(CreateAPIRequest request);
    }
}
