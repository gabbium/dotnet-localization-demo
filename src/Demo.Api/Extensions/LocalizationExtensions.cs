namespace Demo.Api.Extensions;

public static class LocalizationExtensions
{
    public static IServiceCollection AddRequestLocalization(this IServiceCollection services)
    {
        services.AddLocalization();

        var supportedCultures = new[]
        {
            new CultureInfo("en-US"),
            new CultureInfo("pt-BR"),
            new CultureInfo("es-ES")
        };

        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture("en-US");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
            options.ApplyCurrentCultureToResponseHeaders = true;
        });

        return services;
    }

    public static IApplicationBuilder UseRequestLocalizationMiddleware(this IApplicationBuilder app)
    {
        var options = app.ApplicationServices
            .GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;

        app.UseRequestLocalization(options);

        return app;
    }
}
