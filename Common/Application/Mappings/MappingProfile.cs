using AutoMapper;
using Domain.Request.Brands;
using Domain.Response.Brands;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BrandResponse, BrandRequest>().ReverseMap();
        }
    }
}
