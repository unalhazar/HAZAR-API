using Application.Contracts.Persistence;
using Domain.Response.Product;

namespace Application.Features.Products.Commands.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProcessResult<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<ProcessResult<ProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var response = new ProcessResult<ProductResponse>();

            try
            {
                var product = _mapper.Map<Product>(request);
                await _productRepository.UpdateAsync(product);

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
