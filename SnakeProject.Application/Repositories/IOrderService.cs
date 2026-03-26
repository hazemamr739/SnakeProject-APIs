using SnakeProject.Application.DTOs.Orders;

namespace SnakeProject.Application.Repositories;

public interface IOrderService
{
    Task<Result<OrderResponse>> GetOrderAsync(int orderId, CancellationToken cancellationToken);
    Task<Result<PagedResponse<OrderResponse>>> GetUserOrdersAsync(string userId, int page = 1, int pageSize = 20, CancellationToken cancellationToken = default);
    Task<Result<OrderResponse>> CreateOrderFromCartAsync(string userId, CreateOrderRequest request, CancellationToken cancellationToken);
    Task<Result<OrderResponse>> UpdateOrderStatusAsync(int orderId, string newStatus, CancellationToken cancellationToken);
    Task<Result> CancelOrderAsync(int orderId, CancellationToken cancellationToken);
}
