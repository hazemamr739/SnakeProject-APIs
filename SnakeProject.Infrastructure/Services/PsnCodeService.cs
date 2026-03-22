using Mapster;
namespace SnakeProject.Infrastructure.Repositories;

public class PsnCodeService(ApplicationDbContext context, IUnitOfWork _unitOfWork) : IPsnCodeService
{
    private readonly ApplicationDbContext _dbContext = context;
  
    public async Task<IEnumerable<PsnCodeResponse>> GetAllPsnCodeAsync(
        CancellationToken cancellationToken = default)
    {
        var psns = await _dbContext.PsnCodes.ToListAsync(cancellationToken);
        return psns.Adapt<List<PsnCodeResponse>>();
    }
    
    public async Task<Result<PsnCodeResponse>> GetPsnCodeAsync(string id, CancellationToken cancellationToken = default)
    {
        var psnCode = await _dbContext.PsnCodes.FindAsync(id, cancellationToken);

        if (psnCode is null)
            return (Result<PsnCodeResponse>)Result<PsnCodeResponse>.Failure(PsnCodeErrors.PsnCodeNotFound(id));

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

        await _dbContext.PsnCodes.AddAsync(psnCode, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success<PsnCodeResponse>(psnCode.Adapt<PsnCodeResponse>());
    }
    public async Task<Result<PsnCodeResponse>> UpdateAsyn(string id, PsnCodeRequest request, CancellationToken cancellationToken = default)
    {
        var psnCode = await _dbContext.PsnCodes.FindAsync(new object[] { id }, cancellationToken);

        if (psnCode is null)
            return Result.Failure<PsnCodeResponse>(PsnCodeErrors.PsnCodeNotFound(id));

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

        request.Adapt(psnCode);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success<PsnCodeResponse>(psnCode.Adapt<PsnCodeResponse>());
    }

    public async Task<Result> DeleteAsyn(string id, CancellationToken cancellationToken)
    {
        var psnCode = await _dbContext.PsnCodes.FindAsync(id, cancellationToken);

        if (psnCode is null)
            return Result.Failure(PsnCodeErrors.PsnCodeNotFound(id));

        _dbContext.PsnCodes.Remove(psnCode);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }


  
}
