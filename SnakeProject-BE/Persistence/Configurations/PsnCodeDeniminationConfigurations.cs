using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SnakeProject_BE.Persistence.Configurations
{
    public class PsnCodeDeniminationConfigurations : BaseEntityConfiguration<PsnCodesDenomination>
    {
        protected override void ConfigureProperties(EntityTypeBuilder<PsnCodesDenomination> builder)
        {
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Amount)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Currency)
                .IsRequired();

            builder.Property(p => p.RegionId)
                .IsRequired();

            builder.Property(p => p.ProductId)
                .IsRequired();
        }

        protected override void ConfigureRelationships(EntityTypeBuilder<PsnCodesDenomination> builder)
        {
            // Many PsnCodesDenominations belong to One PsnRegion
            builder.HasOne(p => p.Region)
                .WithMany(r => r.PsnCodesDenominations)
                .HasForeignKey(p => p.RegionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PsnCodesDenomination_PsnRegion_RegionId");

            // Many PsnCodesDenominations belong to One Product
            builder.HasOne(p => p.Product)
                .WithMany(pr => pr.Denominations)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PsnCodesDenomination_Product_ProductId");

            // One PsnCodesDenomination has Many PsnCodes
            builder.HasMany(p => p.PsnCodes)
                .WithOne(pc => pc.Denomination)
                .HasForeignKey(pc => pc.DenominationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PsnCode_PsnCodesDenomination_DenominationId");
        }

        protected override void ConfigureIndexes(EntityTypeBuilder<PsnCodesDenomination> builder)
        {
            // Index for ProductId lookups
            builder.HasIndex(p => p.ProductId)
                .HasDatabaseName("IX_PsnCodesDenomination_ProductId");

            // Index for RegionId lookups
            builder.HasIndex(p => p.RegionId)
                .HasDatabaseName("IX_PsnCodesDenomination_RegionId");

            // Composite index for region and product queries
            builder.HasIndex(p => new { p.RegionId, p.ProductId })
                .HasDatabaseName("IX_PsnCodesDenomination_RegionId_ProductId");

            // Composite index for currency filtering by region
            builder.HasIndex(p => new { p.RegionId, p.Currency })
                .HasDatabaseName("IX_PsnCodesDenomination_RegionId_Currency");
        }
    }
}
