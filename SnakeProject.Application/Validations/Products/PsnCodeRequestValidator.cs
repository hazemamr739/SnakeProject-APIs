using SnakeProject.Application.DTOs.Product;

namespace SnakeProject.Application.Validations.Products
{
    public class PsnCodeRequestValidator :AbstractValidator<PsnCodeRequest>
    {
        public PsnCodeRequestValidator()
        {
            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("Code is required.")
                .Length(4, 30).WithMessage("Code must be between 4 and 30 characters.");

            RuleFor(p => p.ProductId)
               .GreaterThan(0).WithMessage("ProductId must be a positive number.");


            RuleFor(x => x.UsedAt)
                .Equal(default(DateTime))
                .WithMessage("UsedAt must be empty when IsUsed is false.");
        }
    }
}
