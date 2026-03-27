using Microsoft.AspNetCore.Identity;

namespace SnakeProject.Domain.Entities
{
    public class ApplicationRole :IdentityRole
    {
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
    }
}
