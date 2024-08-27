using Application.Base;
using Application.Features.Brands.Requests;
using Application.Features.Brands.Responses;

namespace Application.Features.Brands.Commands.Create
{
    public class CreateBrandCommand : BrandRequest, IRequest<ProcessResult<BrandResponse>>
    {
    }
}

