namespace SnakeProject.Application.Repositories;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponse>> GetActiveCategoriesAsync(CancellationToken cancellationToken = default);
}
