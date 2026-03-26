namespace SnakeProject.Application.DTOs.Orders;

public record CreateOrderRequest
{
    public string ShippingAddress { get; init; } = default!;
    public string PaymentMethod { get; init; } = default!;
}
