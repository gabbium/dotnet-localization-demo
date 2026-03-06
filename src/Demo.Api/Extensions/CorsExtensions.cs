namespace Demo.Api.Extensions;

public static class CorsExtensions
{
    private const string SpaCorsPolicy = "SpaCorsPolicy";

    public static IServiceCollection AddSpaCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];

        services.AddCors(options =>
        {
            options.AddPolicy(SpaCorsPolicy, policy =>
            {
                policy.WithOrigins(allowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        return services;
    }

    public static WebApplication UseSpaCorsPolicy(this WebApplication app)
    {
        app.UseCors(SpaCorsPolicy);

        return app;
    }
}
