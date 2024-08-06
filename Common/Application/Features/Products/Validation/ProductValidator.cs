using Domain.Request.Products;

namespace Application.Features.Products.Validation
{
    public class ProductValidator : AbstractValidator<ProductRequest>
    {
        public ProductValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Product name is required.");
            RuleFor(c => c.Name).MinimumLength(10).WithMessage("Product name must be at least 10 characters long."); // Düzeltilmiş kural
            RuleFor(c => c.Price).GreaterThan(0).WithMessage("Product price must be greater than zero.");
            RuleFor(c => c.Stock).GreaterThanOrEqualTo(0).WithMessage("Product stock cannot be negative.");
        }
    }
}
