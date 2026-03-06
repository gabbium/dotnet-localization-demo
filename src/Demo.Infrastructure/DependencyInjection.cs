using Demo.Application.Interfaces;
using Demo.Domain.AggregatesModel.ProductAggregate;
using Demo.Infrastructure.Data;
using Demo.Infrastructure.Data.Interceptors;

namespace Demo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DemoDb");

        ArgumentNullException.ThrowIfNull(connectionString, nameof(connectionString));

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSnakeCaseNamingConvention();

            options.UseNpgsql(connectionString, o =>
            {
                o.MapEnum<ProductStatus>();
            });
        });

        services.AddScoped<ApplicationDbContextInitializer>();

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton(TimeProvider.System);

        return services;
    }
}
