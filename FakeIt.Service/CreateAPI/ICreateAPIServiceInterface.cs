using FakeIt.Common.APIModel.CreateAPI;
using FakeIt.Common.DTOs.CreateAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeIt.Service.CreateAPI
{
    public interface ICreateAPIServiceInterface
    {
        Task<Common.DTOs.CreateAPI.CreateAPIResponse> CreateStaticMapping(Common.DTOs.CreateAPI.CreateAPIRequest request);
    }
}
