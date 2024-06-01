using AutoMapper;
using FakeIt.Common.Mapper;

namespace FakeIt.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            //Mapper for creating static API
            CreateMap<Common.APIModel.CreateAPI.CreateAPIRequest, Common.DTOs.CreateAPI.CreateAPIRequest>()
           .ForMember(dest => dest.Response, 
                              opt => opt.MapFrom(new DynamicToStringResolver<Common.APIModel.CreateAPI.CreateAPIRequest>(src => src.Response)));

            CreateMap<Common.DTOs.CreateAPI.CreateAPIRequest, Common.Entity.CreateAPI.CreateAPIRequest>();

            CreateMap<Common.Entity.CreateAPI.CreateAPIResponse, Common.DTOs.CreateAPI.CreateAPIResponse>();

            CreateMap< Common.DTOs.CreateAPI.CreateAPIResponse, Common.APIModel.CreateAPI.CreateAPIResponse>();
        }
    }
}
