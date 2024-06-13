using Domain.Response.Brands;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandCommand : IRequest<ProcessResult<BrandResponse>>
    {
        public string Name { get; set; } = string.Empty;

    }
}

