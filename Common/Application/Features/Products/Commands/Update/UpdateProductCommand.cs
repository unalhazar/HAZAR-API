using Application.Base;
using Application.Features.Products.Requests;
using Application.Features.Products.Responses;

namespace Application.Features.Products.Commands.Update
{
    public class UpdateProductCommand : ProductRequest, IRequest<ProcessResult<ProductResponse>>
    {
    }
}
