namespace SnakeProject.Infrastructure.Data.Configurations
{
    public class CategoryConfigurations : BaseEntityConfiguration<Category>
    {
        protected override void ConfigureProperties(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.SortOrder)
                .HasDefaultValue(0);

            builder.Property(c => c.IsActive)
                .HasDefaultValue(true);
        }

        protected override void ConfigureIndexes(EntityTypeBuilder<Category> builder)
        {
            builder.HasIndex(c => c.Name)
                .HasDatabaseName("IX_Category_Name");

            builder.HasIndex(c => c.IsActive)
                .HasDatabaseName("IX_Category_IsActive");

            builder.HasIndex(c => c.SortOrder)
                .HasDatabaseName("IX_Category_SortOrder");
        }
    }
}
