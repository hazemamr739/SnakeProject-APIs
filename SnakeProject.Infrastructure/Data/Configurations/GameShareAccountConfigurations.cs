namespace SnakeProject.Infrastructure.Data.Configurations
{
    public class GameShareAccountConfigurations : BaseEntityConfiguration<GameShareAccount>
    {
        protected override void ConfigureProperties(EntityTypeBuilder<GameShareAccount> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.AccessType)
                .IsRequired()
                .HasConversion<byte>();

            builder.Property(x => x.Console)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.Price)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.CategoryId)
                .IsRequired(false);
        }

        protected override void ConfigureRelationships(EntityTypeBuilder<GameShareAccount> builder)
        {
            builder.HasOne(x => x.Category)
                .WithMany(c => c.GameShareAccounts)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_GameShareAccount_Categories_CategoryId");
        }

        protected override void ConfigureIndexes(EntityTypeBuilder<GameShareAccount> builder)
        {
            builder.HasIndex(x => x.CategoryId)
                .HasDatabaseName("IX_GameShareAccount_CategoryId");

            builder.HasIndex(x => x.IsActive)
                .HasDatabaseName("IX_GameShareAccount_IsActive");
        }
    }
}
