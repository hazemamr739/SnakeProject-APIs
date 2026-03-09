using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SnakeProject_BE.Persistence.Configurations
{
    public class RegionCategoryConfigurations : IEntityTypeConfiguration<RegionCategory>
    {
        public void Configure(EntityTypeBuilder<RegionCategory> builder)
        {
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(p => p.PsnRegions)
                .WithOne(r => r.RegionCategory)
                .HasForeignKey(r => r.RegionCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
