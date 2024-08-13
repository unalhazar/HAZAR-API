using Application.Abstraction;

namespace Infrastructure.ElasticSearch
{
    public class ProductElasticIndexer
    {
        private readonly IProductRepository _productRepository;
        private readonly IElasticSearchService _elasticSearchService;

        public ProductElasticIndexer(IProductRepository productRepository, IElasticSearchService elasticSearchService)
        {
            _productRepository = productRepository;
            _elasticSearchService = elasticSearchService;
        }

        public async Task IndexProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            foreach (var product in products)
            {
                await _elasticSearchService.IndexDocumentAsync(product);
            }
        }
    }
}
