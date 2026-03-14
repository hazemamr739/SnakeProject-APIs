using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SnakeProject.Domain.Entities;

namespace SnakeProject_BE.Persistence.Configurations
{
    public class PsnCodeConfigurations : BaseEntityConfiguration<PsnCode>
    {
        protected override void ConfigureProperties(EntityTypeBuilder<PsnCode> builder)
        {
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Code)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.ProductId)
                .IsRequired();

            builder.Property(p => p.DenominationId)
                .IsRequired();

            builder.Property(p => p.IsUsed)
                .HasDefaultValue(false);

            builder.Property(p => p.UsedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }

        protected override void ConfigureRelationships(EntityTypeBuilder<PsnCode> builder)
        {
            // Many PsnCodes belong to One Product
            builder.HasOne(p => p.Product)
                .WithMany(pr => pr.PsnCodes)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PsnCode_Product_ProductId");

            // Many PsnCodes belong to One PsnCodesDenomination
            builder.HasOne(p => p.Denomination)
                .WithMany(d => d.PsnCodes)
                .HasForeignKey(p => p.DenominationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PsnCode_PsnCodesDenomination_DenominationId");
        }

        protected override void ConfigureIndexes(EntityTypeBuilder<PsnCode> builder)
        {
            // Unique index for Code field
            builder.HasIndex(p => p.Code)
                .IsUnique()
                .HasDatabaseName("IX_PsnCode_Code_Unique");

            // Index for ProductId to improve foreign key lookups
            builder.HasIndex(p => p.ProductId)
                .HasDatabaseName("IX_PsnCode_ProductId");

            // Index for DenominationId to improve foreign key lookups
            builder.HasIndex(p => p.DenominationId)
                .HasDatabaseName("IX_PsnCode_DenominationId");

            // Composite index for frequently queried combinations
            builder.HasIndex(p => new { p.ProductId, p.IsUsed })
                .HasDatabaseName("IX_PsnCode_ProductId_IsUsed");
        }
    }
}
