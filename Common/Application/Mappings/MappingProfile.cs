using Application.Features.Brands.Commands.CreateBrand;
using Domain.Request.Brands;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBrandCommand, BrandRequest>().ReverseMap();
        }
    }
}
