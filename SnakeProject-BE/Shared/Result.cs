namespace SnakeProject_BE.Shared
{
    public abstract record Result
    {
        public sealed record Success(object? Data = null) : Result;
        public sealed record Failure(string Message, Exception? Exception = null) : Result;
    }
}
