using FluentValidation.AspNetCore;
using SnakeProject.Application.Validators;
using SnakeProject.Infrastructure;
using SnakeProject.Infrastructure.UnitOfWork;
using System.Reflection;

namespace SnakeProject.API;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructureServices(configuration);
        services.AddMapsterConfig();
        services.AddFluentValidationConfig();

        return services;
    }
    public static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
    {
        services
        .AddFluentValidationAutoValidation()
        .AddValidatorsFromAssembly(typeof(ProductRequestValidator).Assembly);

        return services;
    }
    public static IServiceCollection AddMapsterConfig(this IServiceCollection services)
    {
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMapper>(new Mapper(mappingConfig));

        return services;
    }
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                sqlOptions => sqlOptions.EnableRetryOnFailure())
        );

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IPsnCodeService, PsnCodeService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
