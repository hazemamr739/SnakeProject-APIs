using SnakeProject_BE.Entities.Enums;

namespace SnakeProject_BE.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public ProductType ProductType { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public bool IsActive { get; set; }

        public ICollection<ProductImage> Images { get; set; } = [];
        public ICollection<GameShareAccount> GameShareAccount { get; set; } = [];
        public ICollection<PlusSupscription> PlusSupscription { get; set; } = [];


    }
}
