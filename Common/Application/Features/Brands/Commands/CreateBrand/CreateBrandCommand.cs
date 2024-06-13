using Domain.Response.Brands;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public partial class CreateBrandCommand : IRequest<BrandResponse>
    {
        public string Name { get; set; }

    }
}

