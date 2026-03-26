using SnakeProject.Domain.Enums;

namespace SnakeProject.Domain.Entities
{
    public class PlusSubscription
    {
        public int Id { get; set; }

        public PlusPlan Plan { get; set; }           // Essential, Extra, Deluxe

        public SubscriptionDuration DurationMonths { get; set; }

        public AccessType AccessType { get; set; }
        public int? CategoryId { get; set; }
        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public Category? Category { get; set; }
    }
}
