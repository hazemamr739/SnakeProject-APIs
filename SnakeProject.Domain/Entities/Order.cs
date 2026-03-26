using SnakeProject.Domain.Enums;

namespace SnakeProject.Domain.Entities;

public class Order
{
    public int Id { get; set; }

    public string UserId { get; set; } = default!;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public decimal TotalAmount { get; set; }
    public string ShippingAddress { get; set; } = default!;
    public string PaymentMethod { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }

    public ICollection<OrderItem> Items { get; set; } = [];
}
