using MapsterMapper;
using SnakeProject.Application.Repositories;
using SnakeProject.Infrastructure;
using SnakeProject.Infrastructure.Repositories;
using System.Reflection;

namespace SnakeProject.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Mapster Configuration
            var mappingConfig = TypeAdapterConfig.GlobalSettings;
            mappingConfig.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton<IMapper>(new Mapper(mappingConfig));

            return services;
        }
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions.EnableRetryOnFailure())
            );
            services.AddScoped<IPsnCodeService, PsnCodeRepository>();

            return services;
        }
    }
}
