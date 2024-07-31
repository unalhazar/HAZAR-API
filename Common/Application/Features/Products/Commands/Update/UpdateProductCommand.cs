using Domain.Request.Products;
using Domain.Response.Products;

namespace Application.Features.Products.Commands.Update
{
    public class UpdateProductCommand : ProductRequest, IRequest<ProcessResult<ProductResponse>>
    {
    }
}
