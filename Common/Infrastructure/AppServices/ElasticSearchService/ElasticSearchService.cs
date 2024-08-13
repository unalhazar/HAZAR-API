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
                .DefaultIndex("products"); // Varsayılan indeks adı, bu her varlık için dinamik olabilir

            _elasticClient = new ElasticClient(settings);
        }

        // Belirli bir varlık türünde belge indeksleme
        public async Task IndexDocumentAsync<T>(T document) where T : class
        {
            var response = await _elasticClient.IndexDocumentAsync(document);
            if (!response.IsValid)
            {
                throw new Exception($"Failed to index document: {response.ServerError.Error.Reason}");
            }
        }

        // Belirli bir varlık türünde arama yapma
        public async Task<ISearchResponse<T>> SearchAsync<T>(Func<SearchDescriptor<T>, ISearchRequest> searchDescriptor) where T : class
        {
            var response = await _elasticClient.SearchAsync(searchDescriptor);
            if (!response.IsValid)
            {
                throw new Exception($"Failed to search documents: {response.ServerError.Error.Reason}");
            }

            return response;
        }

        // Basit Arama Metodu
        public async Task<IEnumerable<T>> SimpleSearchAsync<T>(Expression<Func<T, object>> field, string query) where T : class
        {
            var searchResponse = await SearchAsync<T>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(field)
                        .Query(query)
                    )
                )
            );

            return searchResponse.Documents;
        }

        // Gelişmiş Arama Metodu
        public async Task<IEnumerable<T>> AdvancedSearchAsync<T>(Expression<Func<T, object>> field, string query, int? minPrice = null, int? maxPrice = null) where T : class
        {
            var searchResponse = await SearchAsync<T>(s => s
                .Query(q => q
                    .Bool(b => b
                        .Must(
                            m => m.Match(mq => mq.Field(field).Query(query)),
                            m => m.Range(r => r.Field("price").GreaterThanOrEquals(minPrice).LessThanOrEquals(maxPrice))
                        )
                    )
                )
            );

            return searchResponse.Documents;
        }

        public async Task<IEnumerable<T>> SearchByMultipleCriteriaAsync<T>(string query, Expression<Func<T, object>>[] fields, int? minPrice = null, int? maxPrice = null) where T : class
        {
            var searchResponse = await SearchAsync<T>(s => s
                .Query(q => q
                    .Bool(b => b
                        .Must(
                            m => m.MultiMatch(mm => mm
                                .Fields(f => f.Fields(fields.Select(field => new Field(field)).ToArray()))
                                .Query(query)
                            ),
                            m => m.Range(r => r.Field("price").GreaterThanOrEquals(minPrice).LessThanOrEquals(maxPrice))
                        )
                    )
                )
            );

            return searchResponse.Documents;
        }
        public async Task SearchProductsAsync(string query)
        {
            var results = await SimpleSearchAsync<Product>(p => p.Name, query);
        }

        public async Task SearchAdvancedProductsAsync(string query, int minPrice, int maxPrice)
        {
            var results = await AdvancedSearchAsync<Product>(p => p.Name, query, minPrice, maxPrice);
        }

    }
}
