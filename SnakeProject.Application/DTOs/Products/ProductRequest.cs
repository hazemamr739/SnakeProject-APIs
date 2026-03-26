using SnakeProject.Domain.Enums;

namespace SnakeProject.Application.DTOs.Products
{
    public record ProductRequest(
        string Name,
        string Description,
        decimal Price,
        string ImageUrl,
        ProductType Type,
        int? CategoryId = null);
}
