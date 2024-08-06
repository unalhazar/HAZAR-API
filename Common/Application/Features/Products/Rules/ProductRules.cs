using Application.Contracts.Persistence;

namespace Application.Features.Products.Rules
{
    public class ProductRules
    {
        private readonly IProductRepository _productRepository;

        public ProductRules(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProcessResult<Product>> CheckIfProductNameIsUniqueAsync(string productName)
        {
            var existingProduct = await _productRepository.GetByQueryAsync(p => p.Name == productName);
            if (existingProduct != null)
            {
                return new ProcessResult<Product>
                {
                    Durum = false,
                    Mesaj = "Product name must be unique.",
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            return new ProcessResult<Product>
            {
                Durum = true,
                Mesaj = "Product name is unique.",
                HttpStatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }

}
