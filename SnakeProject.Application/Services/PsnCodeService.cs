using SnakeProject.API.Contracts.Product;
using SnakeProject.Domain.Entities;
namespace SnakeProject.Application.Services
{
    public class PsnCodeService(AppDbContext context) : IPsnCodeService
    {
        private readonly AppDbContext _context = context;



        public async Task<IEnumerable<PsnCodeResponse>> GetAllPsnCodesAsync() => (IEnumerable<PsnCodeResponse>)await _context.PsnCodes.ToListAsync();
        public async Task<PsnCode> GetPsnCodeByIdAsync(int id)
        {

            var psnCode = await _context.PsnCodes.FindAsync(id);
            return psnCode ?? throw new KeyNotFoundException($"PsnCode with ID {id} not found.");
        }
        public async Task<PsnCode> AddPsnCodeAsync(PsnCodeRequest codeRequest, CancellationToken cancellationToken = default)
        {
            var existingCode = await _context.PsnCodes
                .FirstOrDefaultAsync(p => p.Code == codeRequest.Code, cancellationToken);

            if (existingCode is not null)
                throw new InvalidOperationException($"PsnCode with code '{codeRequest.Code}' already exists.");

            var psnCode = codeRequest.Adapt<PsnCode>();

            _context.PsnCodes.Add(psnCode);
            await _context.SaveChangesAsync(cancellationToken);

            return psnCode;
        }

        Task<IEnumerable<PsnCodeResponse>> IPsnCodeService.GetAllPsnCodesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PsnCode> AddPsnCodeAsync(PsnCodeRequest codeRequest, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
