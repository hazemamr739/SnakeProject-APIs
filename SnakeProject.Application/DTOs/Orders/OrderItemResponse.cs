namespace SnakeProject.Application.DTOs.Orders;

public record OrderItemResponse
{
    public int Id { get; init; }
    public int ProductId { get; init; }
    public int? PsnCodeId { get; init; }
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal TotalPrice { get; init; }
}
