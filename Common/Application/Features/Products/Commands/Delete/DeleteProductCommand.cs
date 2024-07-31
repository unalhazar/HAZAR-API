using Domain.Response.Products;

namespace Application.Features.Products.Commands.Delete
{
    public class DeleteProductCommand : IRequest<ProcessResult<ProductResponse>>
    {
        public long Id { get; set; }
    }
}
