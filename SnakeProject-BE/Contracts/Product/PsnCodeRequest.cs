namespace SnakeProject_BE.Contracts.Product
{
    public record PsnCodeRequest(string Code, int ProductId, DateTime UsedAt, bool IsUsed = false);
    
}
