using SnakeProject.Domain.Enums;

namespace SnakeProject.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;
        public string Description { get; set; } = string.Empty;
        public ProductType Type { get; set; }
        public int? CategoryId { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = default!;
        public bool IsActive { get; set; }

        public Category? Category { get; set; }
        public ICollection<PsnCodesDenomination> Denominations { get; set; } = [];
        public ICollection<PsnCode> PsnCodes { get; set; } = [];
    }
}
