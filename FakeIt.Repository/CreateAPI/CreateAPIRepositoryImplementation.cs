using FakeIt.Common.Entity.CreateAPI;

namespace FakeIt.Repository.CreateAPI
{
    public class CreateAPIRepositoryImplementation : ICreateAPIRepositoryInterface
    {

        public CreateAPIRepositoryImplementation() 
        {

        }

        public Task<CreateStaticResponse> CreateStaticMapping(CreateStaticRequest request)
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}
