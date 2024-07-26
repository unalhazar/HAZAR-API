using Domain.Response.Brands;

namespace Application.Features.Brands.Queries.GetAllBrand
{
    public class GetAllBrandQuery : IRequest<ProcessResult<List<BrandResponse>>>
    {
    }
}
