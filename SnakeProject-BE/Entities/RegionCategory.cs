namespace SnakeProject_BE.Entities
{
    public class RegionCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<PsnRegion> PsnRegions { get; set; } = [];
    }
}
