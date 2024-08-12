using Domain.Response.Products;

namespace Application.Features.Products.Queries.SearchQuery.Database
{
    public class SearchDatabaseQuery : IRequest<ProcessResult<List<ProductResponse>>>
    {
        // Gerekli filtreleme kriterleri eklenebilir
    }
}
