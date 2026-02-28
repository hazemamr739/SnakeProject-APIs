using SnakeProject_BE.Entities.Enums;

namespace SnakeProject_BE.Entities
{
    public class PlusSupscription
    {
        public int Id { get; set; }

        public PlusPlan Plan { get; set; }           // Essential, Extra, Deluxe

        public SubscriptionDuration DurationMonths { get; set; }    

        public AccessType AccessType { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        
    }
}
