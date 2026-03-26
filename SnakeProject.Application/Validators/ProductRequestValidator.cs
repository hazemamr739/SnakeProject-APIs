using FluentValidation;
using SnakeProject.Application.DTOs.Products;

namespace SnakeProject.Application.Validators;

public class ProductRequestValidator : AbstractValidator<ProductRequest>
{
    public ProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .Length(3, 100).WithMessage("Product name must be between 3 and 100 characters.");

        RuleFor(x => x.Description)
            .Length(0, 500).WithMessage("Product description cannot exceed 500 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Product price must be greater than 0.");

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("Product image URL is required.")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("Product image URL must be a valid URL.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Product type must be a valid enum value.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).When(x => x.CategoryId.HasValue)
            .WithMessage("Category ID must be greater than 0.");
    }
}
