using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SnakeProject_BE.Persistence.Configurations
{
    public class PsnCodeDeniminationConfigurations : IEntityTypeConfiguration<PsnCodesDenomination>
    {
        public void Configure(EntityTypeBuilder<PsnCodesDenomination> builder)
        {
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Currency)
                .IsRequired();

            builder.Property(p => p.RegionId)
                .IsRequired();

            builder.Property(p => p.ProductId)
                .IsRequired();

            builder.HasOne(p => p.Region)
                .WithMany(r => r.PsnCodesDenominations)
                .HasForeignKey(p => p.RegionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
