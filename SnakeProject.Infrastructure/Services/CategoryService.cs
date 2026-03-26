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

    public async Task<Result<CategoryResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var category = await _dbContext.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (category is null)
            return Result.Failure<CategoryResponse>(CategoryErrors.CategoryNotFound(id));

        return Result.Success(category.Adapt<CategoryResponse>());
    }

    public async Task<Result<CategoryResponse>> AddAsync(CategoryRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var normalizedName = request.Name.Trim();

        var duplicateExists = await _dbContext.Categories
            .AnyAsync(x => x.Name == normalizedName, cancellationToken);

        if (duplicateExists)
            return Result.Failure<CategoryResponse>(CategoryErrors.DuplicateCategoryName);

        var category = request.Adapt<Category>();
        category.Name = normalizedName;
        category.IsActive = true;

        await _dbContext.Categories.AddAsync(category, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(category.Adapt<CategoryResponse>());
    }

    public async Task<Result<CategoryResponse>> UpdateAsync(int id, CategoryRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var category = await _dbContext.Categories
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (category is null)
            return Result.Failure<CategoryResponse>(CategoryErrors.CategoryNotFound(id));

        var normalizedName = request.Name.Trim();

        var duplicateExists = await _dbContext.Categories
            .AnyAsync(x => x.Id != id && x.Name == normalizedName, cancellationToken);

        if (duplicateExists)
            return Result.Failure<CategoryResponse>(CategoryErrors.DuplicateCategoryName);

        category.Name = normalizedName;
        category.SortOrder = request.SortOrder;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(category.Adapt<CategoryResponse>());
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var category = await _dbContext.Categories
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (category is null)
            return Result.Failure(CategoryErrors.CategoryNotFound(id));

        if (!category.IsActive)
            return Result.Failure(CategoryErrors.CategoryAlreadyInactive);

        category.IsActive = false;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
