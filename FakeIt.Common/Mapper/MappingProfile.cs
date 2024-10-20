using AutoMapper;

namespace FakeIt.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Mapper for creating static API
            CreateMap<Common.APIModel.CreateAPI.CreateAPIRequest, Common.DTOs.CreateAPI.CreateAPIRequest>();

            CreateMap<Common.DTOs.CreateAPI.CreateAPIRequest, Common.Entity.CreateAPI.CreateAPIRequest>();

            CreateMap<Common.Entity.CreateAPI.CreateAPIResponse, Common.DTOs.CreateAPI.CreateAPIResponse>();

            CreateMap<Common.DTOs.CreateAPI.CreateAPIResponse, Common.APIModel.CreateAPI.CreateAPIResponse>();


            //Mapper for reading API response
            CreateMap<Common.APIModel.ReadAPI.ReadAPIRequest, Common.DTOs.ReadAPI.ReadAPIRequest>();

            CreateMap<Common.DTOs.ReadAPI.ReadAPIRequest, Common.Entity.ReadAPI.ReadAPIRequest>();


            CreateMap<Common.Entity.ReadAPI.ReadAPIResponse, Common.DTOs.ReadAPI.ReadAPIResponse>();

            CreateMap<Common.DTOs.ReadAPI.ReadAPIResponse, Common.APIModel.ReadAPI.ReadAPIResponse>();

        }
    }
}
