using SnakeProject.Domain.Entities;
using SnakeProject_BE.Contracts.Product;
namespace SnakeProject_BE.Services
{
    public interface IPsnCodeService
    {
        public Task<IEnumerable<PsnCodeResponse>> GetAllPsnCodesAsync();
        public Task<PsnCode> GetPsnCodeByIdAsync(int id);
        public Task<PsnCode> AddPsnCodeAsync(PsnCodeRequest codeRequest, CancellationToken cancellationToken = default);

    }
}
