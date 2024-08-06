using Application.Contracts.Persistence;
using Domain.Response.Products;

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
                var products = _productRepository.GetListByIncludes(
                includes: q => q.Include(p => p.Category).Include(p => p.Brand));

                var entities = products.ToList();
                response.Result = _mapper.Map<List<ProductResponse>>(entities);

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
