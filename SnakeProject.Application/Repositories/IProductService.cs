using SnakeProject.Domain.Enums;
namespace SnakeProject.Application.Repositories
{
    public interface IProductService
    {
        Task<PagedResponse<ProductResponse>> GetActiveProductsAsync(
            int? categoryId,
            ProductType? type,
            string? search,
            int page = 1,
            int pageSize = 20,
            string? sortBy = "name",
            bool descending = false,
            CancellationToken cancellationToken = default);

        Task<Result<ProductResponse>> GetProductByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Result<ProductResponse>> AddAsync(ProductRequest request, CancellationToken cancellationToken = default);
        Task<Result<ProductResponse>> UpdateAsync(int id, ProductRequest request, CancellationToken cancellationToken = default);
        Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
