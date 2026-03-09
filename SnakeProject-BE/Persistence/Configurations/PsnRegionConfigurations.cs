using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SnakeProject_BE.Persistence.Configurations
{
    public class PsnRegionConfigurations : IEntityTypeConfiguration<PsnRegion>
    {
        public void Configure(EntityTypeBuilder<PsnRegion> builder)
        {
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.RegionCategoryId)
                .IsRequired();

            builder.HasOne(p => p.RegionCategory)
                .WithMany(rc => rc.PsnRegions)
                .HasForeignKey(p => p.RegionCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.PsnCodesDenominations)
                .WithOne(pcd => pcd.Region)
                .HasForeignKey(pcd => pcd.RegionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
