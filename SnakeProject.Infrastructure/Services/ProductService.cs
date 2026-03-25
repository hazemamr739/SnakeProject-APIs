using Mapster;

namespace SnakeProject.Infrastructure.Services
{
    public class ProductService(ApplicationDbContext context) : IProductService
    {
        private readonly ApplicationDbContext _dbContext = context;

        public async Task<IEnumerable<ProductResponse>> GetActiveProductsAsync(int? categoryId, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Products
                .AsNoTracking()
                .Where(x => x.IsActive);

            if (categoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == categoryId.Value);
            }

            var products = await query
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);

            return products.Adapt<List<ProductResponse>>();
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

            await _dbContext.Products.AddAsync(product, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(product.Adapt<ProductResponse>());
        }
    }
}
