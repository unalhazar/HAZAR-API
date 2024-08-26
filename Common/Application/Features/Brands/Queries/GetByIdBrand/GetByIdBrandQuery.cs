using Application.Base;
using Application.Features.Brands.Responses;

namespace Application.Features.Brands.Queries.GetByIdBrand
{
    public class GetByIdBrandQuery : IRequest<ProcessResult<BrandResponse>>
    {
        public long Id { get; set; }
    }
}
