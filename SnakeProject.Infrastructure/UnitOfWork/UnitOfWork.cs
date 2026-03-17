
namespace SnakeProject.Infrastructure.UnitOfWork
{
    public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public Task<int> CompleteAsync()=> _dbContext.SaveChangesAsync();
    }
}
