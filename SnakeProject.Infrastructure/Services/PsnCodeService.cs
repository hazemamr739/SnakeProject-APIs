using Mapster;
using SnakeProject.Domain.Enums;

namespace SnakeProject.Infrastructure.Repositories;

public class PsnCodeService(ApplicationDbContext context, IUnitOfWork _unitOfWork) : IPsnCodeService
{
    private readonly ApplicationDbContext _dbContext = context;

    public async Task<IEnumerable<PsnCodeResponse>> GetAllPsnCodeAsync(
        InventoryStatus? status = null,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.PsnCodes.AsNoTracking();

        if (status.HasValue)
        {
            query = query.Where(x => x.Status == status.Value);
        }

        var psns = await query.ToListAsync(cancellationToken);
        return psns.Adapt<List<PsnCodeResponse>>();
    }

    public async Task<Result<PsnCodeResponse>> GetPsnCodeAsync(int id, CancellationToken cancellationToken = default)
    {
        var psnCode = await _dbContext.PsnCodes.FindAsync([id], cancellationToken);

        if (psnCode is null)
            return Result.Failure<PsnCodeResponse>(PsnCodeErrors.PsnCodeNotFound(id.ToString()));

        return Result<PsnCodeResponse>.Success(psnCode.Adapt<PsnCodeResponse>());
    }

    public async Task<Result<PsnCodeResponse>> AddAsyn(PsnCodeRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Code))
            return Result.Failure<PsnCodeResponse>(PsnCodeErrors.EmptyPsnCode);

        var productExists = await _dbContext.Products
            .AnyAsync(x => x.Id == request.ProductId, cancellationToken);

        if (!productExists)
            return Result.Failure<PsnCodeResponse>(PsnCodeErrors.ProductNotFound(request.ProductId));

        var denomination = await _dbContext.PsnCodesDenominations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.DenominationId, cancellationToken);

        if (denomination is null)
            return Result.Failure<PsnCodeResponse>(PsnCodeErrors.DenominationNotFound(request.DenominationId));

        if (denomination.ProductId != request.ProductId)
            return Result.Failure<PsnCodeResponse>(PsnCodeErrors.DenominationProductMismatch(request.DenominationId, request.ProductId));

        var exists = await _dbContext.PsnCodes
            .AnyAsync(x => x.Code == request.Code, cancellationToken);

        if (exists)
            return Result.Failure<PsnCodeResponse>(PsnCodeErrors.DuplicatePsnCode);

        var psnCode = request.Adapt<PsnCode>();
        psnCode.Status = request.IsUsed ? InventoryStatus.Sold : InventoryStatus.Available;
        if (request.IsUsed && psnCode.UsedAt == default)
        {
            psnCode.UsedAt = DateTime.UtcNow;
        }

        await _dbContext.PsnCodes.AddAsync(psnCode, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success<PsnCodeResponse>(psnCode.Adapt<PsnCodeResponse>());
    }

    public async Task<Result<PsnCodeResponse>> UpdateAsyn(int id, PsnCodeRequest request, CancellationToken cancellationToken = default)
    {
        var psnCode = await _dbContext.PsnCodes.FindAsync([id], cancellationToken);

        if (psnCode is null)
            return Result.Failure<PsnCodeResponse>(PsnCodeErrors.PsnCodeNotFound(id.ToString()));

        var productExists = await _dbContext.Products
            .AnyAsync(x => x.Id == request.ProductId, cancellationToken);

        if (!productExists)
            return Result.Failure<PsnCodeResponse>(PsnCodeErrors.ProductNotFound(request.ProductId));

        var denomination = await _dbContext.PsnCodesDenominations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.DenominationId, cancellationToken);

        if (denomination is null)
            return Result.Failure<PsnCodeResponse>(PsnCodeErrors.DenominationNotFound(request.DenominationId));

        if (denomination.ProductId != request.ProductId)
            return Result.Failure<PsnCodeResponse>(PsnCodeErrors.DenominationProductMismatch(request.DenominationId, request.ProductId));

        psnCode.Code = request.Code.Trim();
        psnCode.ProductId = request.ProductId;
        psnCode.DenominationId = request.DenominationId;

        if (request.IsUsed)
        {
            psnCode.IsUsed = true;
            psnCode.Status = InventoryStatus.Sold;
            psnCode.UsedAt = request.UsedAt == default ? DateTime.UtcNow : request.UsedAt;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success<PsnCodeResponse>(psnCode.Adapt<PsnCodeResponse>());
    }

    public async Task<Result> DeleteAsyn(int id, CancellationToken cancellationToken)
    {
        var psnCode = await _dbContext.PsnCodes.FindAsync([id], cancellationToken);

        if (psnCode is null)
            return Result.Failure(PsnCodeErrors.PsnCodeNotFound(id.ToString()));

        _dbContext.PsnCodes.Remove(psnCode);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<PsnCodeResponse>> ReserveAsync(int id, CancellationToken cancellationToken = default)
    {
        var psnCode = await _dbContext.PsnCodes.FindAsync([id], cancellationToken);

        if (psnCode is null)
            return Result.Failure<PsnCodeResponse>(PsnCodeErrors.PsnCodeNotFound(id.ToString()));

        if (psnCode.Status != InventoryStatus.Available)
            return Result.Failure<PsnCodeResponse>(PsnCodeErrors.InvalidStatusTransition(psnCode.Status, InventoryStatus.Reserved));

        psnCode.Status = InventoryStatus.Reserved;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(psnCode.Adapt<PsnCodeResponse>());
    }

    public async Task<Result<PsnCodeResponse>> ReserveNextAvailableAsync(int denominationId, CancellationToken cancellationToken = default)
    {
        var denominationExists = await _dbContext.PsnCodesDenominations
            .AsNoTracking()
            .AnyAsync(x => x.Id == denominationId, cancellationToken);

        if (!denominationExists)
            return Result.Failure<PsnCodeResponse>(PsnCodeErrors.DenominationNotFound(denominationId));

        var psnCode = await _dbContext.PsnCodes
            .FirstOrDefaultAsync(x => x.DenominationId == denominationId && x.Status == InventoryStatus.Available, cancellationToken);

        if (psnCode is null)
            return Result.Failure<PsnCodeResponse>(PsnCodeErrors.OutOfStock(denominationId));

        psnCode.Status = InventoryStatus.Reserved;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(psnCode.Adapt<PsnCodeResponse>());
    }

    public async Task<Result<PsnCodeResponse>> ReleaseAsync(int id, CancellationToken cancellationToken = default)
    {
        var psnCode = await _dbContext.PsnCodes.FindAsync([id], cancellationToken);

        if (psnCode is null)
            return Result.Failure<PsnCodeResponse>(PsnCodeErrors.PsnCodeNotFound(id.ToString()));

        if (psnCode.Status != InventoryStatus.Reserved)
            return Result.Failure<PsnCodeResponse>(PsnCodeErrors.InvalidStatusTransition(psnCode.Status, InventoryStatus.Available));

        psnCode.Status = InventoryStatus.Available;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(psnCode.Adapt<PsnCodeResponse>());
    }

    public async Task<Result<PsnCodeResponse>> MarkSoldAsync(int id, CancellationToken cancellationToken = default)
    {
        var psnCode = await _dbContext.PsnCodes.FindAsync([id], cancellationToken);

        if (psnCode is null)
            return Result.Failure<PsnCodeResponse>(PsnCodeErrors.PsnCodeNotFound(id.ToString()));

        if (psnCode.Status != InventoryStatus.Reserved)
            return Result.Failure<PsnCodeResponse>(PsnCodeErrors.InvalidStatusTransition(psnCode.Status, InventoryStatus.Sold));

        psnCode.Status = InventoryStatus.Sold;
        psnCode.IsUsed = true;
        psnCode.UsedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(psnCode.Adapt<PsnCodeResponse>());
    }

    public async Task<PsnInventorySummaryResponse> GetInventorySummaryAsync(int denominationId, CancellationToken cancellationToken = default)
    {
        var groups = await _dbContext.PsnCodes
            .AsNoTracking()
            .Where(x => x.DenominationId == denominationId)
            .GroupBy(x => x.Status)
            .Select(g => new { Status = g.Key, Count = g.Count() })
            .ToListAsync(cancellationToken);

        var available = groups.FirstOrDefault(x => x.Status == InventoryStatus.Available)?.Count ?? 0;
        var reserved = groups.FirstOrDefault(x => x.Status == InventoryStatus.Reserved)?.Count ?? 0;
        var sold = groups.FirstOrDefault(x => x.Status == InventoryStatus.Sold)?.Count ?? 0;
        var total = available + reserved + sold;

        return new PsnInventorySummaryResponse(denominationId, available, reserved, sold, total);
    }
}
