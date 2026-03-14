namespace SnakeProject.Application.DTOs.Product
{
    public record PsnCodeRequest(string Code, int ProductId, DateTime UsedAt, bool IsUsed = false);
    
}
