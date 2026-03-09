

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SnakeProject_BE.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<PsnCodesDenomination> PsnCodesDenominations { get; set; }
        public DbSet<PsnCode> PsnCodes { get; set; }
        public DbSet<PsnRegion> PsnRegions { get; set; }
        public DbSet<RegionCategory> RegionCategories { get; set; }





        //public DbSet<WhatsAppLog> WhatsAppLogs { get; set; }
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderItem> OrderItems { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }
    }


}

