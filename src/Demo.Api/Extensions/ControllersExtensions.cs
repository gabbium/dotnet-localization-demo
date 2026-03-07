using Demo.Api.Infrastructure.Conventions;

namespace Demo.Api.Extensions;

public static class ControllersExtensions
{
    public static IServiceCollection AddApiControllers(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services
            .AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseParameterTransformer()));
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            });

        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        });

        return services;
    }

    public static WebApplication MapApiControllers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
}
