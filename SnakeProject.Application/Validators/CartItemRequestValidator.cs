using FluentValidation;
using SnakeProject.Application.DTOs.Cart;

namespace SnakeProject.Application.Validators;

public class CartItemRequestValidator : AbstractValidator<CartItemRequest>
{
    public CartItemRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("Product ID must be greater than 0.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Quantity cannot exceed 100 items per order.");

        RuleFor(x => x.PsnCodeId)
            .GreaterThan(0).When(x => x.PsnCodeId.HasValue)
            .WithMessage("PSN Code ID must be greater than 0.");
    }
}
