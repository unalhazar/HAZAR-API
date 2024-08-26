using Application.Base;
using Application.Features.Products.Responses;

namespace Application.Features.Products.Commands.Delete
{
    public class DeleteProductCommand : IRequest<ProcessResult<ProductResponse>>
    {
        public long Id { get; set; }
    }
}
