using Application.Abstraction;
using Application.Base;
using Application.Features.Products.Responses;

namespace Application.Features.Products.Queries.SearchQuery.ElasticSearch
{
    public class SearchElasticSearchQueryHandler : IRequestHandler<SearchElasticSearchQuery, ProcessResult<List<ProductResponse>>>
    {
        private readonly IElasticSearchService _elasticSearchService;

        public SearchElasticSearchQueryHandler(IElasticSearchService elasticSearchService)
        {
            _elasticSearchService = elasticSearchService;
        }

        public async Task<ProcessResult<List<ProductResponse>>> Handle(SearchElasticSearchQuery request, CancellationToken cancellationToken)
        {
            ProcessResult<List<ProductResponse>> response = new();

            try
            {
                var searchResponse = await _elasticSearchService.SearchAsync<Product>(s => s
                    .Query(q => q
                        .MatchAll()
                    )
                );




                response.Result = searchResponse.Documents.Select(p => new ProductResponse
                {
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock,
                    CategoryName = p.Category?.Name,
                    BrandName = p.Brand?.Name
                }).ToList();

                response.Durum = true;
                response.Mesaj = "Ürünler başarıyla getirildi.";
                response.HttpStatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Durum = false;
                response.Mesaj = $"Ürünler getirilirken bir hata oluştu: {ex.Message}";
                response.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }


            return response;
        }
    }
}
