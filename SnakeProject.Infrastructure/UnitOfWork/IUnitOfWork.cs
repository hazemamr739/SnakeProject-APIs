namespace SnakeProject.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();
    }
}
