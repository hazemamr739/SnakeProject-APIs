using SnakeProject.API.Contracts.Product;
using SnakeProject.Domain.Entities;
namespace SnakeProject.Application.Services
{
    public interface IPsnCodeService
    {
        public Task<IEnumerable<PsnCodeResponse>> GetAllPsnCodesAsync();
        public Task<PsnCode> GetPsnCodeByIdAsync(int id);
        public Task<PsnCode> AddPsnCodeAsync(PsnCodeRequest codeRequest, CancellationToken cancellationToken = default);

    }
}
