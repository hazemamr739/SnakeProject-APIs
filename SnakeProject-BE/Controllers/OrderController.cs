using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SnakeProject.API.Authentication;

namespace SnakeProject.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class OrderController(IOrderService orderService) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;

    [HasPermission(Permissions.OrdersRead)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOrder(int id, CancellationToken cancellationToken)
    {
        var result = await _orderService.GetOrderAsync(id, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : Problem(statusCode: result.Error.StatusCode ?? StatusCodes.Status404NotFound, title: result.Error.Description);
    }

    [HasPermission(Permissions.OrdersRead)]
    [HttpGet]
    public async Task<IActionResult> GetUserOrders(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var result = await _orderService.GetUserOrdersAsync(userId, page, pageSize, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : Problem(statusCode: result.Error.StatusCode ?? StatusCodes.Status404NotFound, title: result.Error.Description);
    }

    [HasPermission(Permissions.OrdersCreate)]
    [HttpPost]
    public async Task<IActionResult> CreateOrderFromCart([FromBody] OrderRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var result = await _orderService.CreateOrderFromCartAsync(userId, request, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetOrder), new { id = result.Value.Id }, result.Value)
            : Problem(statusCode: result.Error.StatusCode ?? StatusCodes.Status400BadRequest, title: result.Error.Description);
    }

    [HasPermission(Permissions.OrdersUpdateStatus)]
    [HttpPut("{id:int}/status")]
    public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatusRequest request, CancellationToken cancellationToken)
    {
        var result = await _orderService.UpdateOrderStatusAsync(id, request.Status, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : Problem(statusCode: result.Error.StatusCode ?? StatusCodes.Status400BadRequest, title: result.Error.Description);
    }

    [HasPermission(Permissions.OrdersCancel)]
    [HttpDelete("{id:int}/cancel")]
    public async Task<IActionResult> CancelOrder(int id, CancellationToken cancellationToken)
    {
        var result = await _orderService.CancelOrderAsync(id, cancellationToken);
        return result.IsSuccess
            ? NoContent()
            : Problem(statusCode: result.Error.StatusCode ?? StatusCodes.Status400BadRequest, title: result.Error.Description);
    }
}
