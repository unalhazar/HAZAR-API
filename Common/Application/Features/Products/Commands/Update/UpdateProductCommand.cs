using Domain.Request.Products;
using Domain.Response.Product;

namespace Application.Features.Products.Commands.Update
{
    public class UpdateProductCommand : ProductRequest, IRequest<ProcessResult<ProductResponse>>
    {
    }
}
