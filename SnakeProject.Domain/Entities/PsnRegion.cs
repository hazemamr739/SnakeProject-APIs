namespace SnakeProject_BE.Entities
{
    public class PsnRegion
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int RegionCategoryId { get; set; }
        public RegionCategory RegionCategory { get; set; } = null!;

        public ICollection<PsnCodesDenomination> PsnCodesDenominations { get; set; } = [];


    }
}
