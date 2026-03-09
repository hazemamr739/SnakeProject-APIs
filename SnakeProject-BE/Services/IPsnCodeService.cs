using SnakeProject_BE.Contracts.Product;

namespace SnakeProject_BE.Services
{
    public interface IPsnCodeService
    {
        public Task<IEnumerable<PsnCode>> GetAllPsnCodesAsync();
        public Task<PsnCode> GetPsnCodeByIdAsync(int id);
        public Task<PsnCode> AddPsnCodeAsync(PsnCodeRequest code, CancellationToken cancellationToken = default);

    }
}
