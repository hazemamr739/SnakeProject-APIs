using SnakeProject.DomainE.Entities;

namespace SnakeProject.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public int SortOrder { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<PlusSupscription> PlusSupscriptions { get; set; } = [];
        public ICollection<GameShareAccount> GameShareAccounts { get; set; } = [];
        public ICollection<PsnCodesDenomination> PsnCodes { get; set; } = [];

    }
}
