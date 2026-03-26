using SnakeProject.Domain.Enums;

namespace SnakeProject.Application.DTOs.Cart;

public record CheckoutResponse
{
    public int CartId { get; init; }
    public string UserId { get; init; } = default!;
    public CartStatus Status { get; init; }
    public decimal TotalAmount { get; init; }
    public int ItemCount { get; init; }
    public string ShippingAddress { get; init; } = default!;
    public string PaymentMethod { get; init; } = default!;
    public DateTime CheckoutDate { get; init; }
}
