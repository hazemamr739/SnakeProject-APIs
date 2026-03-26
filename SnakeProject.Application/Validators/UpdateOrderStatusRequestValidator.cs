using SnakeProject.Application.DTOs.Orders;
using SnakeProject.Domain.Enums;

namespace SnakeProject.Application.Validators;

public class UpdateOrderStatusRequestValidator : AbstractValidator<UpdateOrderStatusRequest>
{
    public UpdateOrderStatusRequestValidator()
    {
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Order status is required.")
            .Must(status => Enum.TryParse<OrderStatus>(status, ignoreCase: true, out _))
            .WithMessage("Order status must be a valid enum value (Pending, Paid, Processing, Completed, Cancelled, Failed).");
    }
}
