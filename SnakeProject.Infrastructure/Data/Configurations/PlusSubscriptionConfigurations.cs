namespace SnakeProject.Infrastructure.Data.Configurations
{
    public class PlusSubscriptionConfigurations : BaseEntityConfiguration<PlusSubscription>
    {
        protected override void ConfigureProperties(EntityTypeBuilder<PlusSubscription> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Plan)
                .IsRequired()
                .HasConversion<byte>();

            builder.Property(x => x.DurationMonths)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.AccessType)
                .IsRequired()
                .HasConversion<byte>();

            builder.Property(x => x.Price)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.CategoryId)
                .IsRequired(false);
        }

        protected override void ConfigureRelationships(EntityTypeBuilder<PlusSubscription> builder)
        {
            builder.HasOne(x => x.Category)
                .WithMany(c => c.PlusSupscriptions)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_PlusSubscription_Categories_CategoryId");
        }

        protected override void ConfigureIndexes(EntityTypeBuilder<PlusSubscription> builder)
        {
            builder.HasIndex(x => x.CategoryId)
                .HasDatabaseName("IX_PlusSubscription_CategoryId");

            builder.HasIndex(x => x.IsActive)
                .HasDatabaseName("IX_PlusSubscription_IsActive");
        }
    }
}
