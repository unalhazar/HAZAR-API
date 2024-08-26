using Application.Base;
using Application.Contracts.Persistence;
using Application.Features.Products.Queries.SearchQuery.Database;
using Application.Features.Products.Responses;
using Microsoft.EntityFrameworkCore;

public class SearchDatabaseQueryHandler : IRequestHandler<SearchDatabaseQuery, ProcessResult<List<ProductResponse>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public SearchDatabaseQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProcessResult<List<ProductResponse>>> Handle(SearchDatabaseQuery request, CancellationToken cancellationToken)
    {
        ProcessResult<List<ProductResponse>> response = new();

        try
        {
            var complexQuery = _productRepository.GetListByIncludes(
                filter: p => p.Stock < 500 && p.Category != null && p.Brand != null,
                includes: q => q.Include(p => p.Brand)
            );


            var results = await complexQuery.ToListAsync();
            response.Result = _mapper.Map<List<ProductResponse>>(results);
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
