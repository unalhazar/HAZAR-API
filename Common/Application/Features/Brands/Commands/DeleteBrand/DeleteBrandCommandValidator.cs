namespace Application.Features.Brands.Commands.DeleteBrand
{
    public class DeleteBrandCommandValidator : AbstractValidator<Brand>
    {
        public DeleteBrandCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
        }
    }
}
