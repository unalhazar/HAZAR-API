using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Commands.DeleteBrand;
using Application.Features.Brands.Commands.UpdateBrand;
using Application.Features.Categories.Commands.Create;
using Application.Features.Categories.Commands.Update;
using Domain.Request.Brands;
using Domain.Response.Brands;
using Domain.Response.Categories;
using Domain.Response.Products;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBrandCommand, BrandRequest>().ReverseMap();
            CreateMap<CreateBrandCommand, Brand>().ReverseMap();
            CreateMap<CreateCategoryCommand, Category>().ReverseMap();
            CreateMap<UpdateBrandCommand, Brand>().ReverseMap();
            CreateMap<DeleteBrandCommand, Brand>().ReverseMap();
            CreateMap<Brand, BrandResponse>().ReverseMap();
            CreateMap<Product, ProductResponse>().ReverseMap();
            CreateMap<Category, CategoryResponse>().ReverseMap();
            CreateMap<Category, UpdateCategoryCommand>().ReverseMap();
        }
    }
}
