namespace SnakeProject_BE.Services
{
    public interface IPsnCodeService
    {
        public Task<IEnumerable<PsnCode>> GetAllPsnCodesAsync();
        public Task<PsnCode> GetPsnCodeByIdAsync(int id);
        public Task<PsnCode> AddPsnCodeAsync(PsnCode code);

    }
}
