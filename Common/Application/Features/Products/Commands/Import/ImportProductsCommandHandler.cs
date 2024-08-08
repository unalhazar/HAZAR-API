using Application.Abstraction;

namespace Application.Features.Products.Commands.Import
{
    public class ImportProductsCommandHandler : IRequestHandler<ImportProductsCommand, Unit>
    {
        private readonly IProductService _productService;

        public ImportProductsCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<Unit> Handle(ImportProductsCommand request, CancellationToken cancellationToken)
        {
            await _productService.ImportProductsAsync(request.FilePath);
            return Unit.Value;
        }
    }
}
