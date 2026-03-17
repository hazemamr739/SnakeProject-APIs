using Microsoft.AspNetCore.Http;

namespace SnakeProject.Application.ErrorHandling
{
    public static class PsnCodeErrors
    {
        public static readonly Error EmptyPsnCode = new("PsnCode.Empty", "PSN code cannot be empty.", StatusCodes.Status400BadRequest);
        public static Error PsnCodeNotFound(string code) => new("PsnCode.NotFound", $"PSN code '{code}' was not found.", StatusCodes.Status404NotFound);

        public static readonly Error PsnCodeAlreadyRedeemed = new("PsnCode.AlreadyRedeemed", "This code has already been redeemed.", StatusCodes.Status409Conflict);
    }
}