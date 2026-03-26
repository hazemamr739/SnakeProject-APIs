namespace SnakeProject.Application.Repositories;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponse>> GetActiveCategoriesAsync(CancellationToken cancellationToken = default);
    Task<Result<CategoryResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<CategoryResponse>> AddAsync(CategoryRequest request, CancellationToken cancellationToken = default);
    Task<Result<CategoryResponse>> UpdateAsync(int id, CategoryRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
