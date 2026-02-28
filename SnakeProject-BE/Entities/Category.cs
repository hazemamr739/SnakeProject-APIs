namespace SnakeProject_BE.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public int SortOrder { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Product> Products { get; set; } = [];
    }
}
