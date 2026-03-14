namespace SnakeProject.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = default!;
        public bool IsActive { get; set; }
        
        public ICollection<PsnCodesDenomination> Denominations { get; set; } = [];
        public ICollection<PsnCode> PsnCodes { get; set; } = [];
    }
}
