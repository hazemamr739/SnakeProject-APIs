namespace SnakeProject.Application.DTOs.Products;

public record PsnInventorySummaryResponse(
    int DenominationId,
    int Available,
    int Reserved,
    int Sold,
    int Total);
