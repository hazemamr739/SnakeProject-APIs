using Microsoft.AspNetCore.Mvc;
using SnakeProject.Domain.Enums;

namespace SnakeProject_BE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int? categoryId,
        [FromQuery] ProductType? type,
        [FromQuery] string? search,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? sortBy = "name",
        [FromQuery] bool descending = false,
        CancellationToken cancellationToken = default)
    {
        var products = await _productService.GetActiveProductsAsync(categoryId, type, search, page, pageSize, sortBy, descending, cancellationToken);
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _productService.GetProductByIdAsync(id, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : Problem(statusCode: StatusCodes.Status404NotFound, title: result.Error.Description);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ProductRequest request, CancellationToken cancellationToken)
    {
        var result = await _productService.AddAsync(request, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value)
            : Problem(statusCode: result.Error.StatusCode ?? StatusCodes.Status400BadRequest, title: result.Error.Description);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductRequest request, CancellationToken cancellationToken)
    {
        var result = await _productService.UpdateAsync(id, request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : Problem(statusCode: result.Error.StatusCode ?? StatusCodes.Status400BadRequest, title: result.Error.Description);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _productService.DeleteAsync(id, cancellationToken);

        return result.IsSuccess
            ? NoContent()
            : Problem(statusCode: result.Error.StatusCode ?? StatusCodes.Status400BadRequest, title: result.Error.Description);
    }
}
