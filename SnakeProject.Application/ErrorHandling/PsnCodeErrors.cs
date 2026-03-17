using Microsoft.AspNetCore.Http;
using SnakeProject.Application.Abstraction;

namespace SnakeProject.Application.ErrorHandling
{
    public static class PsnCodeErrors
    {
        public static readonly Error EmptyCode = new("PsnCode.Empty", "PSN code cannot be empty.", StatusCodes.Status400BadRequest);
        public static Error NotFound(string code) => new("PsnCode.NotFound", $"PSN code '{code}' was not found.", StatusCodes.Status404NotFound);
        public static readonly Error AlreadyRedeemed = new("PsnCode.AlreadyRedeemed", "This code has already been redeemed.", StatusCodes.Status409Conflict);
    }
}