namespace Application.Features.Brands.Commands.Delete
{
    public class DeleteBrandCommandValidator : AbstractValidator<Brand>
    {
        public DeleteBrandCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
        }
    }
}
