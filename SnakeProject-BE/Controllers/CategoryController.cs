using Microsoft.AspNetCore.Mvc;

namespace SnakeProject_BE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var categories = await _categoryService.GetActiveCategoriesAsync(cancellationToken);
        return Ok(categories);
    }
}
