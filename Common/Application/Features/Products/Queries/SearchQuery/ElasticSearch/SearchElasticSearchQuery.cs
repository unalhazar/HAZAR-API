using Application.Base;
using Application.Features.Products.Responses;

namespace Application.Features.Products.Queries.SearchQuery.ElasticSearch
{
    public class SearchElasticSearchQuery : IRequest<ProcessResult<List<ProductResponse>>>
    {
        // Gerekli filtreleme kriterleri eklenebilir
    }
}
