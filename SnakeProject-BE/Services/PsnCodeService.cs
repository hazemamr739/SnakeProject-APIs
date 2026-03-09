using SnakeProject_BE.Contracts.Product;
using SnakeProject_BE.Persistence;

namespace SnakeProject_BE.Services
{
    public class PsnCodeService(ApplicationDbContext context) : IPsnCodeService
    {
        private readonly ApplicationDbContext _context = context;



        public async Task<IEnumerable<PsnCode>> GetAllPsnCodesAsync() => await _context.PsnCodes.ToListAsync();
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
    }
}
