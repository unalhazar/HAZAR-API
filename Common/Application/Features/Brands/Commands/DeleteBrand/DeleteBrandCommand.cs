using Domain.Response.Brands;

namespace Application.Features.Brands.Commands.DeleteBrand
{
    public class DeleteBrandCommand : IRequest<ProcessResult<BrandResponse>>
    {
        public long Id { get; set; }
    }
}
