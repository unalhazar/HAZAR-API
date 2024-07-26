using Domain.Request.Brands;

namespace Application.Features.Brands.Queries.GetByIdBrand
{
    public class GetByIdBrandQueryValidator : AbstractValidator<BrandRequest>
    {
        public GetByIdBrandQueryValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
        }
    }
}
