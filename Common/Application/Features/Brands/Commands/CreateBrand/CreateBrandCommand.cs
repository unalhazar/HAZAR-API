using Domain.Request.Brands;
using Domain.Response.Brands;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandCommand : BrandRequest, IRequest<BrandResponse>
    {
    }
}
