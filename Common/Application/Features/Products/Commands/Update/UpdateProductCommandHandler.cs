using Application.Abstraction;
using Application.Contracts.Persistence;
using Application.Features.Products.Rules;
using Domain.Response.Products;

namespace Application.Features.Products.Commands.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProcessResult<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly ProductRules _productRules;
        public UpdateProductCommandHandler(IMapper mapper, IProductRepository productRepository, ICacheService cacheService, ProductRules productRules)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _cacheService = cacheService;
            _productRules = productRules;
        }

        public async Task<ProcessResult<ProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var response = new ProcessResult<ProductResponse>();

            try
            {
                // Ürün isminin benzersiz olup olmadığını kontrol edin
                var checkResult = await _productRules.CheckIfProductNameIsUniqueAsync(request.Name);
                if (!checkResult.Durum)
                {
                    response.Durum = checkResult.Durum;
                    response.Mesaj = checkResult.Mesaj;
                    response.HttpStatusCode = checkResult.HttpStatusCode;
                    return response;
                }
                var product = _mapper.Map<Product>(request);
                await _productRepository.UpdateAsync(product);
                // Ürün güncellendikten sonra cache temizle
                await _cacheService.ClearCacheByPrefixAsync("Hazarproducts__1");

                response.Durum = true;
                response.Mesaj = MesajConstats.GuncellemeMesaji;
                response.HttpStatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Durum = false;
                response.Mesaj = $"{MesajConstats.HataMesaji} - {ex.Message}";
                response.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}
