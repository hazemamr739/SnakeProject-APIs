using Microsoft.AspNetCore.Mvc;

namespace SnakeProject_BE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController(ICartService cartService) : ControllerBase
{
    private readonly ICartService _cartService = cartService;

    [HttpGet]
    public async Task<IActionResult> GetCart(CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var result = await _cartService.GetCartAsync(userId, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : Problem(statusCode: result.Error.StatusCode ?? StatusCodes.Status404NotFound, title: result.Error.Description);
    }

    [HttpPost("items")]
    public async Task<IActionResult> AddItem([FromBody] CartItemRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var result = await _cartService.AddItemAsync(userId, request, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetCart), result.Value)
            : Problem(statusCode: result.Error.StatusCode ?? StatusCodes.Status400BadRequest, title: result.Error.Description);
    }

    [HttpDelete("items/{cartItemId:int}")]
    public async Task<IActionResult> RemoveItem(int cartItemId, CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var result = await _cartService.RemoveItemAsync(userId, cartItemId, cancellationToken);
        return result.IsSuccess
            ? NoContent()
            : Problem(statusCode: result.Error.StatusCode ?? StatusCodes.Status404NotFound, title: result.Error.Description);
    }

    [HttpDelete]
    public async Task<IActionResult> ClearCart(CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var result = await _cartService.ClearCartAsync(userId, cancellationToken);
        return result.IsSuccess
            ? NoContent()
            : Problem(statusCode: result.Error.StatusCode ?? StatusCodes.Status404NotFound, title: result.Error.Description);
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var result = await _cartService.CheckoutAsync(userId, request, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : Problem(statusCode: result.Error.StatusCode ?? StatusCodes.Status400BadRequest, title: result.Error.Description);
    }
}
