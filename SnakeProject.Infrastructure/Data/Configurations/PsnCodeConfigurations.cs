using SnakeProject.Domain.Enums;

namespace SnakeProject.Infrastructure.Data.Configurations
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

            builder.Property(p => p.Status)
                .IsRequired()
                .HasConversion<int>()
                .HasDefaultValue(InventoryStatus.Available)
                .HasSentinel((InventoryStatus)0);

            builder.Property(p => p.IsUsed)
                .HasDefaultValue(false);

            builder.Property(p => p.UsedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }

        protected override void ConfigureRelationships(EntityTypeBuilder<PsnCode> builder)
        {
            builder.HasOne(p => p.Product)
                .WithMany(pr => pr.PsnCodes)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PsnCode_Product_ProductId");

            builder.HasOne(p => p.Denomination)
                .WithMany(d => d.PsnCodes)
                .HasForeignKey(p => p.DenominationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PsnCode_PsnCodesDenomination_DenominationId");
        }

        protected override void ConfigureIndexes(EntityTypeBuilder<PsnCode> builder)
        {
            builder.HasIndex(p => p.Code)
                .IsUnique()
                .HasDatabaseName("IX_PsnCode_Code_Unique");

            builder.HasIndex(p => p.ProductId)
                .HasDatabaseName("IX_PsnCode_ProductId");

            builder.HasIndex(p => p.DenominationId)
                .HasDatabaseName("IX_PsnCode_DenominationId");

            builder.HasIndex(p => new { p.ProductId, p.Status })
                .HasDatabaseName("IX_PsnCode_ProductId_Status");
        }
    }
}
