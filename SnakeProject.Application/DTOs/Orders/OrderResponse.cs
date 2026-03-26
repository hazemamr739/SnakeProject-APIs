using SnakeProject.Domain.Enums;

namespace SnakeProject.Application.DTOs.Orders;

public record OrderResponse
{
    public int Id { get; init; }
    public string UserId { get; init; } = default!;
    public OrderStatus Status { get; init; }
    public decimal TotalAmount { get; init; }
    public string ShippingAddress { get; init; } = default!;
    public string PaymentMethod { get; init; } = default!;
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public DateTime? CompletedAt { get; init; }
    public ICollection<OrderItemResponse> Items { get; init; } = [];
}
