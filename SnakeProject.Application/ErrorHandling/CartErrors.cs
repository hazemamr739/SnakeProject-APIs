using Microsoft.AspNetCore.Http;

namespace SnakeProject.Application.ErrorHandling;

public static class CartErrors
{
    public static readonly Error CartNotFound = new("Cart.NotFound", "Cart not found.", StatusCodes.Status404NotFound);
    public static readonly Error CartItemNotFound = new("Cart.ItemNotFound", "Cart item not found.", StatusCodes.Status404NotFound);
    public static readonly Error EmptyCart = new("Cart.Empty", "Cannot checkout with an empty cart.", StatusCodes.Status400BadRequest);
    public static readonly Error InvalidQuantity = new("Cart.InvalidQuantity", "Quantity must be greater than zero.", StatusCodes.Status400BadRequest);
    public static Error ProductNotFound(int productId) => new("Cart.ProductNotFound", $"Product '{productId}' not found.", StatusCodes.Status404NotFound);
    public static Error InsufficientStock(int productId) => new("Cart.InsufficientStock", $"Insufficient stock for product '{productId}'.", StatusCodes.Status409Conflict);
    public static readonly Error PsnCodeAlreadyUsed = new("Cart.PsnCodeAlreadyUsed", "The selected PSN code has already been used.", StatusCodes.Status409Conflict);
    public static readonly Error CartAlreadyConverted = new("Cart.AlreadyConverted", "Cart has already been converted to an order.", StatusCodes.Status409Conflict);
}
