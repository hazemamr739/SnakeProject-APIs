namespace SnakeProject.Infrastructure.Data.Configurations
{
    public class PsnRegionConfigurations : BaseEntityConfiguration<PsnRegion>
    {
        protected override void ConfigureProperties(EntityTypeBuilder<PsnRegion> builder)
        {
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.RegionCategoryId)
                .IsRequired();
        }

        protected override void ConfigureRelationships(EntityTypeBuilder<PsnRegion> builder)
        {
            // Many PsnRegions belong to One RegionCategory
            builder.HasOne(p => p.RegionCategory)
                .WithMany(rc => rc.PsnRegions)
                .HasForeignKey(p => p.RegionCategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PsnRegion_RegionCategory_RegionCategoryId");

            // One PsnRegion has Many PsnCodesDenominations
            builder.HasMany(p => p.PsnCodesDenominations)
                .WithOne(pcd => pcd.Region)
                .HasForeignKey(pcd => pcd.RegionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PsnCodesDenomination_PsnRegion_RegionId");
        }

        protected override void ConfigureIndexes(EntityTypeBuilder<PsnRegion> builder)
        {
            // Index for RegionCategoryId lookups
            builder.HasIndex(p => p.RegionCategoryId)
                .HasDatabaseName("IX_PsnRegion_RegionCategoryId");

            // Index for Name lookups
            builder.HasIndex(p => p.Name)
                .HasDatabaseName("IX_PsnRegion_Name");

            // Composite index for region and category queries
            builder.HasIndex(p => new { p.RegionCategoryId, p.Name })
                .HasDatabaseName("IX_PsnRegion_RegionCategoryId_Name");
        }
    }
}
