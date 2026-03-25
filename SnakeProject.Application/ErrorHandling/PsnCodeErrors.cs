using Microsoft.AspNetCore.Http;
using SnakeProject.Domain.Enums;

namespace SnakeProject.Application.ErrorHandling
{
    public static class PsnCodeErrors
    {
        public static readonly Error EmptyPsnCode = new("PsnCode.Empty", "PSN code cannot be empty.", StatusCodes.Status400BadRequest);
        public static Error PsnCodeNotFound(string code) => new("PsnCode.NotFound", $"PSN code '{code}' was not found.", StatusCodes.Status404NotFound);
        public static Error ProductNotFound(int productId) => new("PsnCode.ProductNotFound", $"Product '{productId}' was not found.", StatusCodes.Status400BadRequest);
        public static Error DenominationNotFound(int denominationId) => new("PsnCode.DenominationNotFound", $"Denomination '{denominationId}' was not found.", StatusCodes.Status400BadRequest);
        public static Error DenominationProductMismatch(int denominationId, int productId) =>
            new("PsnCode.DenominationProductMismatch", $"Denomination '{denominationId}' does not belong to product '{productId}'.", StatusCodes.Status400BadRequest);

        public static readonly Error PsnCodeAlreadyRedeemed = new("PsnCode.AlreadyRedeemed", "This code has already been redeemed.", StatusCodes.Status409Conflict);
        public static readonly Error DuplicatePsnCode = new("PsnCode.Duplicate", "A PSN code with the same value already exists.", StatusCodes.Status409Conflict);

        public static Error OutOfStock(int denominationId) =>
            new("PsnCode.OutOfStock", $"No available PSN codes for denomination '{denominationId}'.", StatusCodes.Status409Conflict);

        public static Error InvalidStatusTransition(InventoryStatus from, InventoryStatus to) =>
            new("PsnCode.InvalidStatusTransition", $"Invalid status transition from '{from}' to '{to}'.", StatusCodes.Status409Conflict);
    }
}
