namespace SnakeProject.Application.DTOs.Orders;

public record UpdateOrderStatusRequest
{
    public string Status { get; init; } = default!;
}
