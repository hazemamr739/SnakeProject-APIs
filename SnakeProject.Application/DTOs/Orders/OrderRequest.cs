namespace SnakeProject.Application.DTOs.Orders;

public record OrderRequest
{
    public string ShippingAddress { get; init; } = default!;
    public string PaymentMethod { get; init; } = default!;
}
