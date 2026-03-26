using SnakeProject.Domain.Enums;

namespace SnakeProject.Application.DTOs.Cart;

public record CartResponse
{
    public int Id { get; init; }
    public string UserId { get; init; } = default!;
    public CartStatus Status { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public ICollection<CartItemResponse> Items { get; init; } = [];
}
