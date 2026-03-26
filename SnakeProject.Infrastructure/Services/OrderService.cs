using Mapster;
using SnakeProject.Application.DTOs.Orders;
using SnakeProject.Domain.Enums;

namespace SnakeProject.Infrastructure.Services;

public class OrderService(ApplicationDbContext context, ICartService cartService) : IOrderService
{
    public async Task<Result<OrderResponse>> GetOrderAsync(int orderId, CancellationToken cancellationToken)
    {
        var order = await context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);

        if (order is null)
            return Result.Failure<OrderResponse>(OrderErrors.OrderNotFound);

        return Result.Success(order.Adapt<OrderResponse>());
    }

    public async Task<Result<PagedResponse<OrderResponse>>> GetUserOrdersAsync(string userId, int page = 1, int pageSize = 20, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(userId);

        var safePage = page < 1 ? 1 : page;
        var safePageSize = pageSize < 1 ? 20 : Math.Min(pageSize, 100);

        var totalCount = await context.Orders
            .CountAsync(o => o.UserId == userId, cancellationToken);

        var orders = await context.Orders
            .Include(o => o.Items)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .Skip((safePage - 1) * safePageSize)
            .Take(safePageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var orderResponses = orders.Adapt<List<OrderResponse>>();
        var pagedResponse = new PagedResponse<OrderResponse>(orderResponses, safePage, safePageSize, totalCount);

        return Result.Success(pagedResponse);
    }

    public async Task<Result<OrderResponse>> CreateOrderFromCartAsync(string userId, OrderRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(userId);
        ArgumentNullException.ThrowIfNull(request);

        // Get active cart for user
        var cartResult = await cartService.GetCartAsync(userId, cancellationToken);
        if (cartResult.IsFailure)
            return Result.Failure<OrderResponse>(OrderErrors.CartNotFound);

        var cart = cartResult.Value;
        if (cart.Items.Count == 0)
            return Result.Failure<OrderResponse>(OrderErrors.EmptyOrder);

        // Create order from cart
        var order = new Order
        {
            UserId = userId,
            Status = OrderStatus.Pending,
            TotalAmount = cart.Items.Sum(i => i.Price * i.Quantity),
            ShippingAddress = request.ShippingAddress,
            PaymentMethod = request.PaymentMethod
        };

        // Create order items from cart items
        var orderItems = new List<OrderItem>();
        foreach (var cartItem in cart.Items)
        {
            var product = await context.Products
                .FirstOrDefaultAsync(p => p.Id == cartItem.ProductId, cancellationToken);

            if (product is null)
                return Result.Failure<OrderResponse>(OrderErrors.ProductNotFound(cartItem.ProductId));

            var orderItem = new OrderItem
            {
                ProductId = cartItem.ProductId,
                PsnCodeId = cartItem.PsnCodeId,
                Quantity = cartItem.Quantity,
                UnitPrice = cartItem.Price,
                TotalPrice = cartItem.Price * cartItem.Quantity
            };

            orderItems.Add(orderItem);
        }

        order.Items = orderItems;
        context.Orders.Add(order);
        await context.SaveChangesAsync(cancellationToken);

        // Clear cart after creating order
        await cartService.ClearCartAsync(userId, cancellationToken);

        return Result.Success(order.Adapt<OrderResponse>());
    }

    public async Task<Result<OrderResponse>> UpdateOrderStatusAsync(int orderId, string newStatus, CancellationToken cancellationToken)
    {
        var order = await context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);

        if (order is null)
            return Result.Failure<OrderResponse>(OrderErrors.OrderNotFound);

        // Validate status transition
        if (!IsValidStatusTransition(order.Status, newStatus))
            return Result.Failure<OrderResponse>(OrderErrors.InvalidStatusTransition(order.Status.ToString(), newStatus));

        order.Status = Enum.Parse<OrderStatus>(newStatus);
        order.UpdatedAt = DateTime.UtcNow;

        if (order.Status == OrderStatus.Completed)
            order.CompletedAt = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(order.Adapt<OrderResponse>());
    }

    public async Task<Result> CancelOrderAsync(int orderId, CancellationToken cancellationToken)
    {
        var order = await context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);

        if (order is null)
            return Result.Failure(OrderErrors.OrderNotFound);

        // Can only cancel pending or paid orders
        if (order.Status != OrderStatus.Pending && order.Status != OrderStatus.Paid)
            return Result.Failure(OrderErrors.CannotCancelOrder);

        order.Status = OrderStatus.Cancelled;
        order.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private static bool IsValidStatusTransition(OrderStatus currentStatus, string newStatusStr)
    {
        if (!Enum.TryParse<OrderStatus>(newStatusStr, out var newStatus))
            return false;

        return currentStatus switch
        {
            OrderStatus.Pending => newStatus is OrderStatus.Paid or OrderStatus.Cancelled or OrderStatus.Failed,
            OrderStatus.Paid => newStatus is OrderStatus.Processing or OrderStatus.Cancelled,
            OrderStatus.Processing => newStatus is OrderStatus.Completed or OrderStatus.Failed,
            OrderStatus.Completed => false,
            OrderStatus.Cancelled => false,
            OrderStatus.Failed => newStatus is OrderStatus.Pending,
            _ => false
        };
    }
}
