namespace SnakeProject.Application.DTOs.Cart;

public record CartItemRequest
{
    public int ProductId { get; init; }
    public int Quantity { get; init; }
    public int? PsnCodeId { get; init; }
}
