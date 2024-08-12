using Application.Abstraction;
using Application.Contracts.Persistence;
using Domain.Response.Products;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ProcessResult<List<ProductResponse>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper, ICacheService cacheService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<ProcessResult<List<ProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            ProcessResult<List<ProductResponse>> response = new();

            try
            {
                string cacheKey = $"products_{request.SearchTerm}_{request.PageNumber}_{request.PageSize}";
                var cachedProducts = await _cacheService.GetCachedDataAsync<List<ProductResponse>>(cacheKey);

                if (cachedProducts != null)
                {
                    response.Result = cachedProducts;
                    response.Durum = true;
                    response.Mesaj = "Ürünler başarıyla getirildi.";
                    response.HttpStatusCode = System.Net.HttpStatusCode.OK;
                    return response;
                }

                // Arama terimini kullanarak filtre oluşturma
                Expression<Func<Product, bool>> filter = null;
                if (!string.IsNullOrEmpty(request.SearchTerm))
                {
                    filter = p => p.Name.Contains(request.SearchTerm);
                }

                var productsQuery = _productRepository.GetListByIncludes(
                    filter: filter,
                    includes: q => q
                    .Include(p => p.Category)
                    .Include(p => p.Brand));

                // Verileri OrderByDescending ile sıralama
                productsQuery = productsQuery.OrderByDescending(p => p.Id);

                // PageNumber ve PageSize belirtilmediyse, tüm verileri getirin
                if (request.PageNumber > 0 && request.PageSize > 0)
                {
                    productsQuery = productsQuery
                        .Skip((request.PageNumber - 1) * request.PageSize)
                        .Take(request.PageSize);
                }

                var pagedProducts = await productsQuery.ToListAsync();
                var productResponses = _mapper.Map<List<ProductResponse>>(pagedProducts);

                // Cache'e ekleme
                await _cacheService.SetCacheDataAsync(cacheKey, productResponses, TimeSpan.FromMinutes(30));

                response.Result = productResponses;
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
