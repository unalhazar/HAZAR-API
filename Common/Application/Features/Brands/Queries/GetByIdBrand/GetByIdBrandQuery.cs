using Domain.Response.Brands;

namespace Application.Features.Brands.Queries.GetByIdBrand
{
    public class GetByIdBrandQuery : IRequest<ProcessResult<BrandResponse>>
    {
        public long Id { get; set; }
    }
}
