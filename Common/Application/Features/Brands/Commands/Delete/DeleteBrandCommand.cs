using Domain.Response.Brands;

namespace Application.Features.Brands.Commands.Delete
{
    public class DeleteBrandCommand : IRequest<ProcessResult<BrandResponse>>
    {
        public long Id { get; set; }
    }
}
