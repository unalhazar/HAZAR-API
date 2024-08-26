using Application.Base;
using Application.Features.Products.Responses;

namespace Application.Features.Products.Queries.SearchQuery.Database
{
    public class SearchDatabaseQuery : IRequest<ProcessResult<List<ProductResponse>>>
    {
        // Gerekli filtreleme kriterleri eklenebilir
    }
}
