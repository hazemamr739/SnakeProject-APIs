using Mapster;

namespace SnakeProject.Infrastructure.Repositories;

public class CategoryService(ApplicationDbContext context) : ICategoryService
{
    private readonly ApplicationDbContext _dbContext = context;

    public async Task<IEnumerable<CategoryResponse>> GetActiveCategoriesAsync(CancellationToken cancellationToken = default)
    {
        var categories = await _dbContext.Categories
            .AsNoTracking()
            .Where(x => x.IsActive)
            .OrderBy(x => x.SortOrder)
            .ThenBy(x => x.Name)
            .ToListAsync(cancellationToken);

        return categories.Adapt<List<CategoryResponse>>();
    }
}
