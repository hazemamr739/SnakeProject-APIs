using SnakeProject_BE.Persistence;

namespace SnakeProject_BE.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> GetAllAsync();

    }
}
