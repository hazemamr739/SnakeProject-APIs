using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SnakeProject.Domain.Entities;
using SnakeProject.Infrastructure.Data.Configurations;

namespace SnakeProject.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<PsnCodesDenomination> PsnCodesDenominations { get; set; }
    public DbSet<PsnCode> PsnCodes { get; set; }
    public DbSet<PsnRegion> PsnRegions { get; set; }
    public DbSet<RegionCategory> RegionCategories { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CategoryConfigurations());
        modelBuilder.ApplyConfiguration(new ProductConfigurations());
        modelBuilder.ApplyConfiguration(new PsnCodeConfigurations());
        modelBuilder.ApplyConfiguration(new PsnCodeDeniminationConfigurations());
        modelBuilder.ApplyConfiguration(new RegionCategoryConfigurations());
        modelBuilder.ApplyConfiguration(new PsnRegionConfigurations());
        modelBuilder.ApplyConfiguration(new GameShareAccountConfigurations());
        modelBuilder.ApplyConfiguration(new PlusSubscriptionConfigurations());
        modelBuilder.ApplyConfiguration(new CartConfigurations());
        modelBuilder.ApplyConfiguration(new CartItemConfigurations());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}
