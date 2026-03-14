namespace SnakeProject.Infrastructure.Data.Configurations
{
    public class RegionCategoryConfigurations : BaseEntityConfiguration<RegionCategory>
    {
        protected override void ConfigureProperties(EntityTypeBuilder<RegionCategory> builder)
        {
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
        }

        protected override void ConfigureRelationships(EntityTypeBuilder<RegionCategory> builder)
        {
            // One RegionCategory has Many PsnRegions
            builder.HasMany(p => p.PsnRegions)
                .WithOne(r => r.RegionCategory)
                .HasForeignKey(r => r.RegionCategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PsnRegion_RegionCategory_RegionCategoryId");
        }

        protected override void ConfigureIndexes(EntityTypeBuilder<RegionCategory> builder)
        {
            // Index for Name lookups (commonly searched field)
            builder.HasIndex(p => p.Name)
                .IsUnique()
                .HasDatabaseName("IX_RegionCategory_Name_Unique");
        }
    }
}
