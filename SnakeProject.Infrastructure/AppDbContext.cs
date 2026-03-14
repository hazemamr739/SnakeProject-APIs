using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SnakeProject_BE.Persistence.Configurations;
namespace SnakeProject.Infrastructure;
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<PsnCodesDenomination> PsnCodesDenominations { get; set; }
        public DbSet<PsnCode> PsnCodes { get; set; }    
        public DbSet<PsnRegion> PsnRegions { get; set; }
        public DbSet<RegionCategory> RegionCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductConfigurations());
            modelBuilder.ApplyConfiguration(new PsnCodeConfigurations());
            modelBuilder.ApplyConfiguration(new PsnCodeDeniminationConfigurations());
            modelBuilder.ApplyConfiguration(new RegionCategoryConfigurations());
            modelBuilder.ApplyConfiguration(new PsnRegionConfigurations());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }


