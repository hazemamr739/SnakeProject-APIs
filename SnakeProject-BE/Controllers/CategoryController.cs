using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SnakeProject.API.Authentication;

namespace SnakeProject.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;

    [HasPermission(Permissions.CategoriesRead)]
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var categories = await _categoryService.GetActiveCategoriesAsync(cancellationToken);
        return Ok(categories);
    }


    [HasPermission(Permissions.CategoriesRead)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetByIdAsync(id, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : Problem(statusCode: result.Error.StatusCode ?? StatusCodes.Status404NotFound, title: result.Error.Description);
    }

    [Authorize]
    [HasPermission(Permissions.CategoriesAdd)]
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CategoryRequest request, CancellationToken cancellationToken)
    {
        var result = await _categoryService.AddAsync(request, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value)
            : Problem(statusCode: result.Error.StatusCode ?? StatusCodes.Status400BadRequest, title: result.Error.Description);
    }


    [Authorize]
    [HasPermission(Permissions.CategoriesUpdate)]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CategoryRequest request, CancellationToken cancellationToken)
    {
        var result = await _categoryService.UpdateAsync(id, request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : Problem(statusCode: result.Error.StatusCode ?? StatusCodes.Status400BadRequest, title: result.Error.Description);
    }


    [Authorize]
    [HasPermission(Permissions.CategoriesDelete)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _categoryService.DeleteAsync(id, cancellationToken);

        return result.IsSuccess
            ? NoContent()
            : Problem(statusCode: result.Error.StatusCode ?? StatusCodes.Status400BadRequest, title: result.Error.Description);
    }
}
