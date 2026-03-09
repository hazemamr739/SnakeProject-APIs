using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SnakeProject_BE.Persistence.Configurations
{
    public class PsnCodeConfigurations : IEntityTypeConfiguration<PsnCode>
    {
        public void Configure(EntityTypeBuilder<PsnCode> builder)
        {
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Code)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.ProductId)
                .IsRequired();

            builder.Property(p => p.IsUsed)
                .HasDefaultValue(false);

            builder.Property(p => p.UsedAt)
                .HasDefaultValue(DateTime.UtcNow);

            builder.HasIndex(p => p.Code)
                .IsUnique();

            builder.HasOne(p => p.Product)
                .WithMany(pr => pr.PsnCodes)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
