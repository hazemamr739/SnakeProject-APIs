
namespace SnakeProject.Application.DTOs.Products;

public record PsnCodeRequest
{
    public string Code { get; init; } = string.Empty;
    public int ProductId { get; init; }
    public int DenominationId { get; init; }
    public DateTime? UsedAt { get; init; }
    public bool IsUsed { get; init; }
}
