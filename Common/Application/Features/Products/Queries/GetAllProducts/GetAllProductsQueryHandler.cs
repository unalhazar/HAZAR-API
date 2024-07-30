using Application.Contracts.Persistence;
using Domain.Response.Product;

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
                using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30), System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                {
                    var products = await _productRepository.GetAllAsync();
                    var entities = products.ToList();
                    response.Result = _mapper.Map<List<ProductResponse>>(entities);
                    Transaction.Complete();
                }

                response.Durum = true;
                response.Mesaj = MesajConstats.EklemeMesaji;
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
