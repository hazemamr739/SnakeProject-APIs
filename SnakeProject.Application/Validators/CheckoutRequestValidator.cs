namespace SnakeProject.Application.Validators;

public class CheckoutRequestValidator : AbstractValidator<CheckoutRequest>
{
    public CheckoutRequestValidator()
    {
        RuleFor(x => x.ShippingAddress)
            .NotEmpty().WithMessage("Shipping address is required.")
            .Length(5, 500).WithMessage("Shipping address must be between 5 and 500 characters.");

        RuleFor(x => x.PaymentMethod)
            .NotEmpty().WithMessage("Payment method is required.")
            .Length(2, 100).WithMessage("Payment method must be between 2 and 100 characters.");
    }
}
