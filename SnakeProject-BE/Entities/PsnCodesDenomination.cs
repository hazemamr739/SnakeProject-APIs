using SnakeProject_BE.Entities.Enums;

namespace SnakeProject_BE.Entities
{
    public class PsnCodesDenomination
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }

        public Currency Currency { get; set; }
        public int RegionId { get; set; }
        public PsnRegion Region { get; set; } = null!;
        public Product Product { get; set; } = null!;

    }
}
