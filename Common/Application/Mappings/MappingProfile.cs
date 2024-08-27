using Application.Features.Brands.Commands.Delete;
using Application.Features.Brands.Requests;
using Application.Features.Brands.Responses;
using Application.Features.Categories.Commands.Create;
using Application.Features.Categories.Commands.Update;
using Application.Features.Categories.Responses;
using Application.Features.Products.Commands.Create;
using Application.Features.Products.Requests;
using Application.Features.Products.Responses;

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
            CreateMap<Features.Brands.Commands.Create.CreateBrandCommand, Brand>().ReverseMap();
            CreateMap<Features.Brands.Commands.Update.UpdateBrandCommand, Brand>().ReverseMap();
            CreateMap<DeleteBrandCommand, Brand>().ReverseMap();
            CreateMap<Brand, BrandResponse>().ReverseMap();
            CreateMap<Features.Brands.Commands.Create.CreateBrandCommand, BrandRequest>().ReverseMap();
            #endregion

            #region Category
            CreateMap<Category, CategoryResponse>().ReverseMap();
            CreateMap<Category, UpdateCategoryCommand>().ReverseMap();
            CreateMap<CreateCategoryCommand, Category>().ReverseMap();
            #endregion





        }
    }

}
