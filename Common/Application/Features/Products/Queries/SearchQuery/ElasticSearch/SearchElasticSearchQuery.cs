using Domain.Response.Products;

namespace Application.Features.Products.Queries.SearchQuery.ElasticSearch
{
    public class SearchElasticSearchQuery : IRequest<ProcessResult<List<ProductResponse>>>
    {
        // Gerekli filtreleme kriterleri eklenebilir
    }
}
