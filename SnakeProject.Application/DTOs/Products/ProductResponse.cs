using SnakeProject.Domain.Enums;

namespace SnakeProject.Application.DTOs.Products;

public record ProductResponse(
    int Id,
    string Name,
    string Description,
    decimal Price,
    string ImageUrl,
    ProductType Type,
    int? CategoryId
);
