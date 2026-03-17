using Mapster;
namespace SnakeProject.Infrastructure.Repositories;

public class PsnCodeService(ApplicationDbContext context, IUnitOfWork _unitOfWork) : IPsnCodeService
{
    private readonly ApplicationDbContext _dbContext = context;
    //public async Task<IReadOnlyList<PsnCode>> GetAllAsync(CancellationToken cancellationToken = default) =>
    //    await context.PsnCodes.ToListAsync(cancellationToken);

    //public Task<PsnCode?> FindAsync(int id, CancellationToken cancellationToken = default) =>
    //    context.PsnCodes.FindAsync([id], cancellationToken).AsTask();

    //public Task<PsnCode?> GetByCodeAsync(string code, CancellationToken cancellationToken = default) =>
    //    context.PsnCodes.FirstOrDefaultAsync(p => p.Code == code, cancellationToken);
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

    public Task<Result<PsnCodeResponse>> AddAsyn(PsnCodeRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
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


    public Task<Result<PsnCodeResponse>> UpdateAsyn(int id, PsnCodeRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
