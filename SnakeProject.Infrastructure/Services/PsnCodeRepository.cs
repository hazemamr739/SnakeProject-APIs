using SnakeProject.Application.Repositories;
namespace SnakeProject.Infrastructure.Repositories;

public class PsnCodeRepository(ApplicationDbContext context) : IPsnCodeRepository
{
    public async Task<IReadOnlyList<PsnCode>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await context.PsnCodes.ToListAsync(cancellationToken);

    public Task<PsnCode?> FindAsync(int id, CancellationToken cancellationToken = default) =>
        context.PsnCodes.FindAsync([id], cancellationToken).AsTask();

    public Task<PsnCode?> GetByCodeAsync(string code, CancellationToken cancellationToken = default) =>
        context.PsnCodes.FirstOrDefaultAsync(p => p.Code == code, cancellationToken);

    public void Add(PsnCode psnCode) => context.PsnCodes.Add(psnCode);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        context.SaveChangesAsync(cancellationToken);
}
