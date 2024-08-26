using Application.Base;
using Application.Features.Brands.Responses;

namespace Application.Features.Brands.Commands.Delete
{
    public class DeleteBrandCommand : IRequest<ProcessResult<BrandResponse>>
    {
        public long Id { get; set; }
    }
}
