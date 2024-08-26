using Application.Base;
using Application.Features.Products.Requests;
using Application.Features.Products.Responses;

namespace Application.Features.Products.Commands.Create
{
    public class CreateProductCommand : ProductRequest, IRequest<ProcessResult<ProductResponse>>
    {
    }
}
