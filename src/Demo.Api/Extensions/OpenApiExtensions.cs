using Demo.Api.Infrastructure.OpenApi;

namespace Demo.Api.Extensions;

public static class OpenApiExtensions
{
    public static IServiceCollection AddOpenApiWithTransformers(
        this IServiceCollection services,
        params string[] versions)
    {
        foreach (var documentName in versions)
        {
            services.AddOpenApi(
                documentName,
                options =>
                {
                    options.AddDocumentTransformer<OpenApiVersioningDocumentTransformer>();
                    options.AddDocumentTransformer((document, context, cancellationToken) =>
                    {
                        document.Servers = [];
                        return Task.CompletedTask;
                    });
                }
            );
        }

        return services;
    }

    public static WebApplication MapOpenApiAndScalar(this WebApplication app)
    {
        app.MapOpenApi();

        app.MapScalarApiReference(scalarOptions =>
        {
            scalarOptions.WithTheme(ScalarTheme.Kepler);
            scalarOptions.DisableDefaultFonts();
        });

        app.MapGet("/", () => Results.Redirect("/scalar/v1"))
            .ExcludeFromDescription()
            .ExcludeFromApiReference();

        return app;
    }
}
