namespace FakeIt.Service.CreateAPI
{
    public interface ICreateAPIServiceInterface
    {
        Task<Common.DTOs.CreateAPI.CreateAPIResponse> CreateStaticMapping(Common.DTOs.CreateAPI.CreateAPIRequest request);
    }
}
