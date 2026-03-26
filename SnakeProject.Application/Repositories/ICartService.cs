using SnakeProject.Application.DTOs.Cart;

namespace SnakeProject.Application.Repositories;

public interface ICartService
{
    Task<Result<CartResponse>> GetCartAsync(string userId, CancellationToken cancellationToken);
    Task<Result<CartItemResponse>> AddItemAsync(string userId, CartItemRequest request, CancellationToken cancellationToken);
    Task<Result> RemoveItemAsync(string userId, int cartItemId, CancellationToken cancellationToken);
    Task<Result> ClearCartAsync(string userId, CancellationToken cancellationToken);
    Task<Result<CheckoutResponse>> CheckoutAsync(string userId, CheckoutRequest request, CancellationToken cancellationToken);
}
