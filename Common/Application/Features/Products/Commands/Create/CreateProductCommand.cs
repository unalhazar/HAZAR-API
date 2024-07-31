using Domain.Request.Products;
using Domain.Response.Products;

namespace Application.Features.Products.Commands.Create
{
    public class CreateProductCommand : ProductRequest, IRequest<ProcessResult<ProductResponse>>
    {
    }
}
