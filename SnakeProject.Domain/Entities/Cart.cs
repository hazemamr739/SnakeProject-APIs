using SnakeProject.Domain.Enums;

namespace SnakeProject.Domain.Entities;

public class Cart
{
    public int Id { get; set; }

    public string UserId { get; set; } = default!;
    public CartStatus Status { get; set; } = CartStatus.Active;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<CartItem> Items { get; set; } = [];
}
