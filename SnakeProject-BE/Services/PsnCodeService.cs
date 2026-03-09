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
        public Task<PsnCode> AddPsnCodeAsync(PsnCodeRequest codeRequest)
        {
            
        }
    }
}
