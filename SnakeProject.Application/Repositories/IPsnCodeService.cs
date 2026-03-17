using SnakeProject.Domain.Entities;

namespace SnakeProject.Application.Repositories;

public interface IPsnCodeService
{
    Task<IReadOnlyList<PsnCode>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PsnCode?> FindAsync(int id, CancellationToken cancellationToken = default);
    Task<PsnCode?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    void Add(PsnCode psnCode);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
