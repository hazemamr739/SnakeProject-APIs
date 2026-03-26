namespace SnakeProject.Application.DTOs.Cart;

public record CartItemResponse
{
    public int Id { get; init; }
    public int ProductId { get; init; }
    public int? PsnCodeId { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
}
