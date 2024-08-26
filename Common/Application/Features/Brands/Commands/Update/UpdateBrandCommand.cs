using Application.Base;
using Application.Features.Brands.Requests;
using Application.Features.Brands.Responses;

namespace Application.Features.Brands.Commands.Update
{
    public class UpdateBrandCommand : BrandRequest, IRequest<ProcessResult<BrandResponse>>
    {
    }
}
