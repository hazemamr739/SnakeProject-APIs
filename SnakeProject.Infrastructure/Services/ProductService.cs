using Mapster;
using SnakeProject.Domain.Enums;

namespace SnakeProject.Infrastructure.Services
{
    public class ProductService(ApplicationDbContext context) : IProductService
    {
        private readonly ApplicationDbContext _dbContext = context;

        public async Task<PagedResponse<ProductResponse>> GetActiveProductsAsync(
            int? categoryId,
            ProductType? type,
            string? search,
            int page = 1,
            int pageSize = 20,
            string? sortBy = "name",
            bool descending = false,
            CancellationToken cancellationToken = default)
        {
            var safePage = page < 1 ? 1 : page;
            var safePageSize = pageSize < 1 ? 20 : Math.Min(pageSize, 100);
            var sort = string.IsNullOrWhiteSpace(sortBy) ? "name" : sortBy.Trim().ToLowerInvariant();

            var query = _dbContext.Products
                .AsNoTracking()
                .Where(x => x.IsActive);

            if (categoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == categoryId.Value);
            }

            if (type.HasValue)
            {
                query = query.Where(x => x.Type == type.Value);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var searchTerm = search.Trim();
                query = query.Where(x => x.Name.Contains(searchTerm));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            query = (sort, descending) switch
            {
                ("price", false) => query.OrderBy(x => x.Price).ThenBy(x => x.Id),
                ("price", true) => query.OrderByDescending(x => x.Price).ThenByDescending(x => x.Id),
                ("id", false) => query.OrderBy(x => x.Id),
                ("id", true) => query.OrderByDescending(x => x.Id),
                (_, false) => query.OrderBy(x => x.Name).ThenBy(x => x.Id),
                (_, true) => query.OrderByDescending(x => x.Name).ThenByDescending(x => x.Id)
            };

            var products = await query
                .Skip((safePage - 1) * safePageSize)
                .Take(safePageSize)
                .ToListAsync(cancellationToken);

            var items = products.Adapt<List<ProductResponse>>();

            return new PagedResponse<ProductResponse>(items, safePage, safePageSize, totalCount);
        }

        public async Task<Result<ProductResponse>> GetProductByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var product = await _dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (product is null)
                return Result.Failure<ProductResponse>(ProductErrors.ProductNotFound(id));

            return Result.Success(product.Adapt<ProductResponse>());
        }

        public async Task<Result<ProductResponse>> AddAsync(ProductRequest request, CancellationToken cancellationToken = default)
        {
            if (request.CategoryId.HasValue)
            {
                var categoryExists = await _dbContext.Categories
                    .AnyAsync(x => x.Id == request.CategoryId.Value, cancellationToken);

                if (!categoryExists)
                    return Result.Failure<ProductResponse>(ProductErrors.CategoryNotFound(request.CategoryId.Value));
            }

            var normalizedName = request.Name.Trim();

            var duplicateExists = await _dbContext.Products
                .AnyAsync(x => x.Name == normalizedName && x.CategoryId == request.CategoryId, cancellationToken);

            if (duplicateExists)
                return Result.Failure<ProductResponse>(ProductErrors.DuplicateProductName);

            var product = request.Adapt<Product>();
            product.Name = normalizedName;
            product.IsActive = true;

            await _dbContext.Products.AddAsync(product, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(product.Adapt<ProductResponse>());
        }

        public async Task<Result<ProductResponse>> UpdateAsync(int id, ProductRequest request, CancellationToken cancellationToken = default)
        {
            var product = await _dbContext.Products
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (product is null)
                return Result.Failure<ProductResponse>(ProductErrors.ProductNotFound(id));

            if (request.CategoryId.HasValue)
            {
                var categoryExists = await _dbContext.Categories
                    .AnyAsync(x => x.Id == request.CategoryId.Value, cancellationToken);

                if (!categoryExists)
                    return Result.Failure<ProductResponse>(ProductErrors.CategoryNotFound(request.CategoryId.Value));
            }

            var normalizedName = request.Name.Trim();

            var duplicateExists = await _dbContext.Products
                .AnyAsync(x => x.Id != id && x.Name == normalizedName && x.CategoryId == request.CategoryId, cancellationToken);

            if (duplicateExists)
                return Result.Failure<ProductResponse>(ProductErrors.DuplicateProductName);

            request.Adapt(product);
            product.Name = normalizedName;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(product.Adapt<ProductResponse>());
        }

        public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var product = await _dbContext.Products
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (product is null)
                return Result.Failure(ProductErrors.ProductNotFound(id));

            if (!product.IsActive)
                return Result.Failure(ProductErrors.ProductAlreadyInactive);

            product.IsActive = false;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
