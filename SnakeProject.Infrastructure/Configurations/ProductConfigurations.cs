using SnakeProject.Infrastructure.Configurations;

namespace SnakeProject_BE.Persistence.Configurations
{
    public class ProductConfigurations : BaseEntityConfiguration<Product>
    {
        protected override void ConfigureProperties(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Description)
                .HasMaxLength(2500);

            builder.Property(p => p.Price)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.ImageUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(p => p.IsActive)
                .HasDefaultValue(true);
        }

        protected override void ConfigureRelationships(EntityTypeBuilder<Product> builder)
        {
            // One Product has Many PsnCodesDenominations
            builder.HasMany(p => p.Denominations)
                .WithOne(d => d.Product)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PsnCodesDenomination_Product_ProductId");

            // One Product has Many PsnCodes
            builder.HasMany(p => p.PsnCodes)
                .WithOne(pc => pc.Product)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PsnCode_Product_ProductId");
        }

        protected override void ConfigureIndexes(EntityTypeBuilder<Product> builder)
        {
            builder.HasIndex(p => p.Name)
                .HasDatabaseName("IX_Product_Name");

            builder.HasIndex(p => p.IsActive)
                .HasDatabaseName("IX_Product_IsActive");
        }
    }
}
