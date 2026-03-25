using SnakeProject.Application.DTOs.Products;

namespace SnakeProject_BE.Validations.Products
{
    public class ProductRequestValidator : AbstractValidator<ProductRequest>
    {
        public ProductRequestValidator()
        {
            RuleFor(P => P.Name)
                 .NotEmpty().WithMessage("Name is required.")
                 .MaximumLength(250).WithMessage("Name cannot exceed 250 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(2500)
                .WithMessage("Description cannot exceed 2500 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("ImageUrl is required.")
                .MaximumLength(500).WithMessage("ImageUrl cannot exceed 500 characters.");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Invalid product type.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .When(x => x.CategoryId.HasValue)
                .WithMessage("CategoryId must be greater than zero.");
        }
    }
}
