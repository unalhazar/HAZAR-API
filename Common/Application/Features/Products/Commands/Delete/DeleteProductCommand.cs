using Domain.Response.Product;

namespace Application.Features.Products.Commands.Delete
{
    public class DeleteProductCommand : IRequest<ProcessResult<ProductResponse>>
    {
    }
}
