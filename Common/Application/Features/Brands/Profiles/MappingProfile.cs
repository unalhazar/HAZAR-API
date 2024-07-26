using Application.Features.Brands.Commands.CreateBrand;
using Domain.Request.Brands;
using Domain.Response.Brands;

namespace Application.Features.Brands.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBrandCommand, BrandRequest>().ReverseMap();
            CreateMap<Brand, BrandResponse>().ReverseMap();
        }
    }
}
