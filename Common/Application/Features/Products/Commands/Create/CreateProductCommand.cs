using Domain.Request.Products;
using Domain.Response.Product;

namespace Application.Features.Products.Commands.Create
{
    public class CreateProductCommand : ProductRequest, IRequest<ProcessResult<ProductResponse>>
    {
    }
}
