using SnakeProject.Domain.Entities;
namespace SnakeProject.Application.Repositories
{
    public interface IPsnCodeService
    {
        public Task<IEnumerable<PsnCodeResponse>> GetAllPsnCodesAsync();
        public Task<PsnCode> GetPsnCodeByIdAsync(int id);
        public Task<PsnCode> AddPsnCodeAsync(PsnCodeRequest codeRequest, CancellationToken cancellationToken = default);

    }
}
