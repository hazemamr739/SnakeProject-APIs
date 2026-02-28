using SnakeProject_BE.Entities.Enums;

namespace SnakeProject_BE.Entities
{
    public class GameShareAccount
    {
        public int Id { get; set; }

        public AccessType AccessType { get; set; }   // Primary, Secondary, Full
        public ConsoleType Console { get; set; }     // PS4, PS5, Both

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        
    }
}
