using Application.Contracts.Persistence;
using Application.Features.Products.Queries.GetById;
using Domain.Response.Product;

namespace Application.Features.Products.Queries.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, ProcessResult<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetByIdProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProcessResult<ProductResponse>> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
        {
            ProcessResult<ProductResponse> response = new();

            try
            {

                var product = await _productRepository.GetByIdAsync(request.Id);
                response.Result = _mapper.Map<ProductResponse>(product);


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
