namespace Application.Features.Brands.Commands.Create
{
    public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
    {
        public CreateBrandCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(10).WithMessage("Name cannot exceed 10 characters.");

        }
    }
}
