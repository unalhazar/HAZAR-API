namespace Application.Features.Brands.Commands.Update
{
    public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
    {
        public UpdateBrandCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(10).WithMessage("Name cannot exceed 10 characters.");
        }
    }
}
