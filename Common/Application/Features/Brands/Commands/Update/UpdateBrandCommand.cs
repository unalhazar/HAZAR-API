using Domain.Request.Brands;
using Domain.Response.Brands;

namespace Application.Features.Brands.Commands.Update
{
    public class UpdateBrandCommand : BrandRequest, IRequest<ProcessResult<BrandResponse>>
    {
    }
}
