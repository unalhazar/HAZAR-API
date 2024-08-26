using Application.Features.Brands.Commands.Create;
using Application.Features.Brands.Commands.Delete;
using Application.Features.Brands.Commands.Update;
using Application.Features.Categories.Commands.Create;
using Application.Features.Categories.Commands.Update;
using Application.Features.Products.Commands.Create;
using Domain.Request.Brands;
using Domain.Request.Products;
using Domain.Response.Brands;
using Domain.Response.Categories;
using Domain.Response.Products;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Product
            CreateMap<CreateProductCommand, ProductRequest>().ReverseMap();
            CreateMap<CreateProductCommand, Product>().ReverseMap();
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name));
            CreateMap<Product, ProductRequest>().ReverseMap();
            #endregion

            #region Brand
            CreateMap<Features.Brands.Commands.Create.UpdateBrandCommand, Brand>().ReverseMap();
            CreateMap<Features.Brands.Commands.Update.UpdateBrandCommand, Brand>().ReverseMap();
            CreateMap<DeleteBrandCommand, Brand>().ReverseMap();
            CreateMap<Brand, BrandResponse>().ReverseMap();
            CreateMap<Features.Brands.Commands.Create.UpdateBrandCommand, BrandRequest>().ReverseMap();
            #endregion

            #region Category
            CreateMap<Category, CategoryResponse>().ReverseMap();
            CreateMap<Category, UpdateCategoryCommand>().ReverseMap();
            CreateMap<CreateCategoryCommand, Category>().ReverseMap();
            #endregion





        }
    }

}
