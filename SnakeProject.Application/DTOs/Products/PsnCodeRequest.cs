namespace SnakeProject.Application.DTOs.Products
{
    public record PsnCodeRequest(string Code, int ProductId, DateTime UsedAt, bool IsUsed = false);
    
}
