using SnakeProject.Domain.Enums;

namespace SnakeProject.Domain.Entities
{
    public class GameShareAccount
    {
        public int Id { get; set; }

        public AccessType AccessType { get; set; }   // Primary, Secondary, Full
        public ConsoleType Console { get; set; }     // PS4, PS5, Both
        public int? CategoryId { get; set; }
        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public Category? Category { get; set; }
    }
}
