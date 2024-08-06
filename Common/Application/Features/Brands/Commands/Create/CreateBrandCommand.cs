using Domain.Request.Brands;
using Domain.Response.Brands;

namespace Application.Features.Brands.Commands.Create
{
    public class CreateBrandCommand : BrandRequest, IRequest<ProcessResult<BrandResponse>>
    {
    }
}

