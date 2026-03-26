using SnakeProject.Domain.Enums;

namespace SnakeProject.Application.Repositories;

public interface IPsnCodeService
{
    Task<IEnumerable<PsnCodeResponse>> GetAllPsnCodeAsync(InventoryStatus? status = null, CancellationToken cancellationToken = default);
    Task<Result<PsnCodeResponse>> GetPsnCodeAsync(int id, CancellationToken cancellationToken = default);

    Task<Result<PsnCodeResponse>> AddAsyn(PsnCodeRequest request, CancellationToken cancellationToken = default);
    Task<Result<PsnCodeResponse>> UpdateAsyn(int id, PsnCodeRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsyn(int id, CancellationToken cancellationToken = default);

    Task<Result<PsnCodeResponse>> ReserveAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<PsnCodeResponse>> ReserveNextAvailableAsync(int denominationId, CancellationToken cancellationToken = default);
    Task<Result<PsnCodeResponse>> ReleaseAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<PsnCodeResponse>> MarkSoldAsync(int id, CancellationToken cancellationToken = default);

    Task<PsnInventorySummaryResponse> GetInventorySummaryAsync(int denominationId, CancellationToken cancellationToken = default);
}
