namespace SnakeProject.Domain.Entities
{
    public class RegionCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public ICollection<PsnRegion> PsnRegions { get; set; } = [];
    }
}
