using FakeIt.Common.Entity.CreateAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeIt.Repository.CreateAPI
{
    public interface ICreateAPIRepositoryInterface
    {
        Task<CreateStaticResponse> CreateStaticMapping(CreateStaticRequest request);
    }
}
