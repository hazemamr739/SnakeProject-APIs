namespace SnakeProject.Application.DTOs.Cart;

public record CheckoutRequest
{
    public string ShippingAddress { get; init; } = default!;
    public string PaymentMethod { get; init; } = default!;
}
