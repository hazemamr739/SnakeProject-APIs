using SnakeProject.Domain.Enums;

namespace SnakeProject.Application.DTOs.Products
{
    public record PsnCodeResponse(int Id, string Code, InventoryStatus Status, bool IsUsed);
   
}
