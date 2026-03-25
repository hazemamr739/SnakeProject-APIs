using SnakeProject.Application.Abstraction;

namespace SnakeProject.Application.Repositories
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetActiveProductsAsync(int? categoryId, CancellationToken cancellationToken = default);
        Task<Result<ProductResponse>> GetProductByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Result<ProductResponse>> AddAsync(ProductRequest request, CancellationToken cancellationToken = default);
    }
}
