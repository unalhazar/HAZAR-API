using Application.Contracts.Persistence;
using Domain.Response.Products;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ProcessResult<List<ProductResponse>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProcessResult<List<ProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            ProcessResult<List<ProductResponse>> response = new();

            try
            {
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

                var totalCount = await productsQuery.CountAsync();
                var pagedProducts = await productsQuery
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                response.Result = _mapper.Map<List<ProductResponse>>(pagedProducts.ToList());

                response.Durum = true;
                response.Mesaj = MesajConstats.GetAllMesajı;
                response.HttpStatusCode = System.Net.HttpStatusCode.OK;
            }

            catch (Exception)
            {
                response.Durum = false;
                response.Mesaj = MesajConstats.HataMesaji;
                response.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return response;

        }
    }

}
