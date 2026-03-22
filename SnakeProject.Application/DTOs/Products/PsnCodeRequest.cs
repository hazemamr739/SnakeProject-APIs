namespace SnakeProject.Application.DTOs.Products
{
    public record PsnCodeRequest(string Code, int ProductId, int DenominationId, DateTime UsedAt, bool IsUsed = false);
    
}
