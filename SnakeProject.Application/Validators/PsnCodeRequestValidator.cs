namespace SnakeProject.Application.Validators;

public class PsnCodeRequestValidator : AbstractValidator<PsnCodeRequest>
{
    public PsnCodeRequestValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("PSN code is required.")
            .Length(5, 50).WithMessage("PSN code must be between 5 and 50 characters.");

        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("Product ID must be greater than 0.");

        RuleFor(x => x.DenominationId)
            .GreaterThan(0).WithMessage("Denomination ID must be greater than 0.");

        RuleFor(x => x.UsedAt)
            .LessThanOrEqualTo(DateTime.UtcNow).When(x => x.IsUsed)
            .WithMessage("Used date cannot be in the future.");
    }
}
