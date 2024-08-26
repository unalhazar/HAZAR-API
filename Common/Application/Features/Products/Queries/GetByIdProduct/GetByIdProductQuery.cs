using Application.Base;
using Application.Features.Products.Responses;

namespace Application.Features.Products.Queries.GetById
{
    public class GetByIdProductQuery : IRequest<ProcessResult<ProductResponse>>
    {
        public long Id { get; set; }
    }
}
