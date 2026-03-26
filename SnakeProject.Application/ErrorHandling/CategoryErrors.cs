using Microsoft.AspNetCore.Http;

namespace SnakeProject.Application.ErrorHandling;

public static class CategoryErrors
{
    public static Error CategoryNotFound(int id) =>
        new("Category.NotFound", $"Category '{id}' was not found.", StatusCodes.Status404NotFound);

    public static readonly Error DuplicateCategoryName =
        new("Category.DuplicateName", "A category with the same name already exists.", StatusCodes.Status409Conflict);

    public static readonly Error CategoryAlreadyInactive =
        new("Category.AlreadyInactive", "Category is already inactive.", StatusCodes.Status409Conflict);
}
