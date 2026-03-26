using Mapster;
using SnakeProject.Domain.Enums;

namespace SnakeProject.Infrastructure.Services;

public class CartService(ApplicationDbContext context, IPsnCodeService psnCodeService) : ICartService
{
    public async Task<Result<CartResponse>> GetCartAsync(string userId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(userId);

        var cart = await context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId && c.Status == CartStatus.Active, cancellationToken);

        if (cart is null)
            return Result.Failure<CartResponse>(CartErrors.CartNotFound);

        return Result.Success(cart.Adapt<CartResponse>());
    }

    public async Task<Result<CartItemResponse>> AddItemAsync(string userId, CartItemRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(userId);
        ArgumentNullException.ThrowIfNull(request);

        if (request.Quantity <= 0)
            return Result.Failure<CartItemResponse>(CartErrors.InvalidQuantity);

        var product = await context.Products
            .FirstOrDefaultAsync(p => p.Id == request.ProductId && p.IsActive, cancellationToken);

        if (product is null)
            return Result.Failure<CartItemResponse>(CartErrors.ProductNotFound(request.ProductId));

        // Get or create active cart for user
        var cart = await context.Carts
            .FirstOrDefaultAsync(c => c.UserId == userId && c.Status == CartStatus.Active, cancellationToken);

        if (cart is null)
        {
            cart = new Cart { UserId = userId, Status = CartStatus.Active };
            context.Carts.Add(cart);
            await context.SaveChangesAsync(cancellationToken);
        }

        // If PSN code product, validate and reserve the PSN code
        if (request.PsnCodeId.HasValue)
        {
            var psnCode = await context.PsnCodes
                .FirstOrDefaultAsync(p => p.Id == request.PsnCodeId.Value, cancellationToken);

            if (psnCode is null || psnCode.ProductId != product.Id)
                return Result.Failure<CartItemResponse>(CartErrors.ProductNotFound(request.ProductId));

            if (psnCode.Status != InventoryStatus.Available)
                return Result.Failure<CartItemResponse>(CartErrors.PsnCodeAlreadyUsed);

            // Reserve the PSN code
            var reserveResult = await psnCodeService.ReserveAsync(psnCode.Id, cancellationToken);
            if (reserveResult.IsFailure)
                return Result.Failure<CartItemResponse>(reserveResult.Error);
        }

        var cartItem = new CartItem
        {
            CartId = cart.Id,
            ProductId = request.ProductId,
            PsnCodeId = request.PsnCodeId,
            Quantity = request.Quantity,
            Price = product.Price
        };

        context.CartItems.Add(cartItem);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(cartItem.Adapt<CartItemResponse>());
    }

    public async Task<Result> RemoveItemAsync(string userId, int cartItemId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(userId);

        var cart = await context.Carts
            .FirstOrDefaultAsync(c => c.UserId == userId && c.Status == CartStatus.Active, cancellationToken);

        if (cart is null)
            return Result.Failure(CartErrors.CartNotFound);

        var cartItem = await context.CartItems
            .FirstOrDefaultAsync(ci => ci.Id == cartItemId && ci.CartId == cart.Id, cancellationToken);

        if (cartItem is null)
            return Result.Failure(CartErrors.CartItemNotFound);

        // Release the PSN code if it was reserved
        if (cartItem.PsnCodeId.HasValue)
        {
            var releaseResult = await psnCodeService.ReleaseAsync(cartItem.PsnCodeId.Value, cancellationToken);
            if (releaseResult.IsFailure)
                return releaseResult;
        }

        context.CartItems.Remove(cartItem);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> ClearCartAsync(string userId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(userId);

        var cart = await context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId && c.Status == CartStatus.Active, cancellationToken);

        if (cart is null)
            return Result.Failure(CartErrors.CartNotFound);

        // Release all reserved PSN codes
        foreach (var item in cart.Items.Where(i => i.PsnCodeId.HasValue))
        {
            var releaseResult = await psnCodeService.ReleaseAsync(item.PsnCodeId.Value, cancellationToken);
            if (releaseResult.IsFailure)
                return releaseResult;
        }

        context.CartItems.RemoveRange(cart.Items);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<CheckoutResponse>> CheckoutAsync(string userId, CheckoutRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(userId);
        ArgumentNullException.ThrowIfNull(request);

        var cart = await context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId && c.Status == CartStatus.Active, cancellationToken);

        if (cart is null)
            return Result.Failure<CheckoutResponse>(CartErrors.CartNotFound);

        if (cart.Items.Count == 0)
            return Result.Failure<CheckoutResponse>(CartErrors.EmptyCart);

        if (cart.Status != CartStatus.Active)
            return Result.Failure<CheckoutResponse>(CartErrors.CartAlreadyConverted);

        // Mark all reserved PSN codes as Sold
        foreach (var item in cart.Items.Where(i => i.PsnCodeId.HasValue))
        {
            var markSoldResult = await psnCodeService.MarkSoldAsync(item.PsnCodeId!.Value, cancellationToken);
            if (markSoldResult.IsFailure)
                return Result.Failure<CheckoutResponse>(markSoldResult.Error);
        }

        // Mark cart as converted
        cart.Status = CartStatus.Converted;
        cart.UpdatedAt = DateTime.UtcNow;
        await context.SaveChangesAsync(cancellationToken);

        var totalAmount = cart.Items.Sum(i => i.Price * i.Quantity);
        var checkoutResponse = new CheckoutResponse
        {
            CartId = cart.Id,
            UserId = cart.UserId,
            Status = cart.Status,
            TotalAmount = totalAmount,
            ItemCount = cart.Items.Count,
            ShippingAddress = request.ShippingAddress,
            PaymentMethod = request.PaymentMethod,
            CheckoutDate = DateTime.UtcNow
        };

        return Result.Success(checkoutResponse);
    }
}
