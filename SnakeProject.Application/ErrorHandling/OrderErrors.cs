using Microsoft.AspNetCore.Http;

namespace SnakeProject.Application.ErrorHandling;

public static class OrderErrors
{
    public static readonly Error OrderNotFound = new("Order.NotFound", "Order not found.", StatusCodes.Status404NotFound);
    public static readonly Error OrderItemNotFound = new("Order.ItemNotFound", "Order item not found.", StatusCodes.Status404NotFound);
    public static readonly Error EmptyOrder = new("Order.Empty", "Cannot create order with no items.", StatusCodes.Status400BadRequest);
    public static readonly Error InvalidOrderStatus = new("Order.InvalidStatus", "Invalid order status.", StatusCodes.Status400BadRequest);
    public static Error InvalidStatusTransition(string from, string to) => new("Order.InvalidStatusTransition", $"Cannot transition from '{from}' to '{to}'.", StatusCodes.Status409Conflict);
    public static Error ProductNotFound(int productId) => new("Order.ProductNotFound", $"Product '{productId}' not found.", StatusCodes.Status404NotFound);
    public static Error InsufficientStock(int productId) => new("Order.InsufficientStock", $"Insufficient stock for product '{productId}'.", StatusCodes.Status409Conflict);
    public static readonly Error CartNotFound = new("Order.CartNotFound", "Active cart not found for user.", StatusCodes.Status404NotFound);
    public static readonly Error OrderAlreadyProcessed = new("Order.AlreadyProcessed", "Order has already been processed.", StatusCodes.Status409Conflict);
    public static readonly Error CannotCancelOrder = new("Order.CannotCancel", "Order cannot be cancelled in its current status.", StatusCodes.Status409Conflict);
}
