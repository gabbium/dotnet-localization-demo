using Demo.Api.Extensions;

namespace Demo.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRequestLocalization();
        services.AddApiVersioningAndExplorer();
        services.AddOpenApiWithTransformers("v1");
        services.AddApiControllers();
        services.AddSpaCorsPolicy(configuration);
        services.AddExceptionHandling();

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseRequestLocalizationMiddleware();
        app.UseExceptionHandling();
        app.UseRouting();
        app.UseSpaCorsPolicy();
        app.MapApiControllers();
        app.MapOpenApiAndScalar();

        return app;
    }
}

