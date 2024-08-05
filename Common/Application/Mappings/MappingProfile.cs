using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Commands.DeleteBrand;
using Application.Features.Brands.Commands.UpdateBrand;
using Domain.Request.Brands;
using Domain.Response.Brands;
using Domain.Response.Products;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBrandCommand, BrandRequest>().ReverseMap();
            CreateMap<CreateBrandCommand, Brand>().ReverseMap();
            CreateMap<UpdateBrandCommand, Brand>().ReverseMap();
            CreateMap<DeleteBrandCommand, Brand>().ReverseMap();
            CreateMap<Brand, BrandResponse>().ReverseMap();
            CreateMap<Product, ProductResponse>().ReverseMap();
        }
    }
}
