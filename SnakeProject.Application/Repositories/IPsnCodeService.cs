
namespace SnakeProject.Application.Repositories;

public interface IPsnCodeService
{
    Task<IEnumerable<PsnCodeResponse>> GetAllPsnCodeAsync(CancellationToken cancellationToken = default);
    Task<Result<PsnCodeResponse>> GetPsnCodeAsync(string id, CancellationToken cancellationToken = default);

    Task<Result<PsnCodeResponse>> AddAsyn(PsnCodeRequest request, CancellationToken cancellationToken = default);
    Task<Result<PsnCodeResponse>> UpdateAsyn(int id, PsnCodeRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsyn(string id, CancellationToken cancellationToken = default);

}
