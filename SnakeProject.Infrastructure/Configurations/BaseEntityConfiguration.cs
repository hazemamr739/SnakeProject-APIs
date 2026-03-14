namespace SnakeProject.Infrastructure.Configurations
{
    public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
    {
     
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            ConfigureKeys(builder);
            ConfigureProperties(builder);
            ConfigureRelationships(builder);
            ConfigureIndexes(builder);
        }

        
        protected virtual void ConfigureKeys(EntityTypeBuilder<TEntity> builder) { }

        protected abstract void ConfigureProperties(EntityTypeBuilder<TEntity> builder);

       
        protected virtual void ConfigureRelationships(EntityTypeBuilder<TEntity> builder) { }

        protected virtual void ConfigureIndexes(EntityTypeBuilder<TEntity> builder) { }
    }
}
