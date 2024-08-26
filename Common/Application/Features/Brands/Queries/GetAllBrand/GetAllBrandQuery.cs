using Application.Base;
using Application.Features.Brands.Responses;

namespace Application.Features.Brands.Queries.GetAllBrand
{
    public class GetAllBrandQuery : IRequest<ProcessResult<List<BrandResponse>>>
    {
    }
}
