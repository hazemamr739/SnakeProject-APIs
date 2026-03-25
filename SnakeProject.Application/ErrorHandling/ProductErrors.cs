using Microsoft.AspNetCore.Http;

namespace SnakeProject.Application.ErrorHandling;

public static class ProductErrors
{
    public static Error ProductNotFound(int id) =>
        new("Product.NotFound", $"Product '{id}' was not found.", StatusCodes.Status404NotFound);

    public static Error CategoryNotFound(int categoryId) =>
        new("Product.CategoryNotFound", $"Category '{categoryId}' was not found.", StatusCodes.Status400BadRequest);

    public static readonly Error DuplicateProductName =
        new("Product.DuplicateName", "A product with the same name already exists in this category.", StatusCodes.Status409Conflict);
}
