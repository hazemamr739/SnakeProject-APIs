using SnakeProject_BE.Entities.Enums;

namespace SnakeProject_BE.Entities
{
    public class PsnCodes
    {
        
        public int Id { get; set; }

        public Region Region { get; set; }

        public PsnDenomination Denomination { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
