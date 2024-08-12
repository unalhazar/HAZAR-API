using Application.Abstraction;
using Application.Contracts.Persistence;
using Application.EventHandlers;
using Application.Features.Products.Rules;
using Domain.Response.Products;

namespace Application.Features.Products.Commands.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProcessResult<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ProductRules _productRules;
        private readonly IMediator _mediator;
        private readonly ICacheService _cacheService;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, ProductRules productRules, IMediator mediator, ICacheService cacheService)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _productRules = productRules;
            _mediator = mediator;
            _cacheService = cacheService;
        }

        public async Task<ProcessResult<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
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
                var addedProduct = await _productRepository.AddAsync(product);
                // Ürün eklendikten sonra cache temizle
                await _cacheService.ClearCacheByPrefixAsync("Hazarproducts__1");
                await _mediator.Publish(new ProductCreatedEvent(product.Id, product.Name), cancellationToken);

                response.Result = _mapper.Map<ProductResponse>(addedProduct);
                response.Durum = true;
                response.Mesaj = MesajConstats.EklemeMesaji;
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
