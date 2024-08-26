using Application.Base;
using Application.Features.Products.Responses;

namespace Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<ProcessResult<List<ProductResponse>>>
    {
        public string? SearchTerm { get; set; }
        public int PageNumber { get; set; } = -1;
        public int PageSize { get; set; } = -1;
    }
}
