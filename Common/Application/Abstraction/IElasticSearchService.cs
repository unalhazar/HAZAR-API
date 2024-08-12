using Nest;

namespace Application.Abstraction
{
    public interface IElasticSearchService
    {
        Task IndexDocumentAsync<T>(T document) where T : class;
        Task<ISearchResponse<T>> SearchAsync<T>(Func<SearchDescriptor<T>, ISearchRequest> searchDescriptor) where T : class;
        Task SearchProductsAsync(string query);
        Task SearchAdvancedProductsAsync(string query, int minPrice, int maxPrice);
    }
}
