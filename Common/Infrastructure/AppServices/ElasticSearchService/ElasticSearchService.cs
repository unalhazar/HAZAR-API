using Application.Abstraction;
using Nest;

namespace Infrastructure.AppServices.ElasticSearchService
{
    public class ElasticSearchService : IElasticSearchService
    {
        private readonly IElasticClient _elasticClient;

        public ElasticSearchService()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("products"); // Varsayılan indeks adı

            _elasticClient = new ElasticClient(settings);
        }

        public async Task IndexDocumentAsync<T>(T document) where T : class
        {
            var response = await _elasticClient.IndexDocumentAsync(document);
            if (!response.IsValid)
            {
                // Hata yönetimi
                throw new Exception($"Failed to index document: {response.ServerError.Error.Reason}");
            }
        }

        public async Task<ISearchResponse<T>> SearchAsync<T>(Func<SearchDescriptor<T>, ISearchRequest> searchDescriptor) where T : class
        {
            var response = await _elasticClient.SearchAsync(searchDescriptor);
            if (!response.IsValid)
            {
                // Hata yönetimi
                throw new Exception($"Failed to search documents: {response.ServerError.Error.Reason}");
            }

            return response;
        }

        // Basit Arama Metodu
        public async Task SearchProductsAsync(string query)
        {
            var searchResponse = await SearchAsync<Product>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Name)
                        .Query(query)
                    )
                )
            );

            foreach (var product in searchResponse.Documents)
            {
                Console.WriteLine($"Product Found: {product.Name}, Price: {product.Price}");
            }
        }

        // Gelişmiş Arama Metodu
        public async Task SearchAdvancedProductsAsync(string query, int minPrice, int maxPrice)
        {
            var searchResponse = await SearchAsync<Product>(s => s
                .Query(q => q
                    .Bool(b => b
                        .Must(
                            m => m.Match(mq => mq.Field(f => f.Name).Query(query)),
                            m => m.Range(r => r.Field(f => f.Price).GreaterThanOrEquals(minPrice).LessThanOrEquals(maxPrice))
                        )
                    )
                )
            );

            foreach (var product in searchResponse.Documents)
            {
                Console.WriteLine($"Product Found: {product.Name}, Price: {product.Price}");
            }
        }
    }
}
