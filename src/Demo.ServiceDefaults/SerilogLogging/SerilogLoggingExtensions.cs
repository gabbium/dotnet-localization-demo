namespace Demo.ServiceDefaults.SerilogLogging;

public static class SerilogLoggingExtensions
{
    public static IHostApplicationBuilder AddDefaultSerilog(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSerilog((sp, loggerConfiguration) =>
        {
            loggerConfiguration.ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                   .WithDefaultDestructurers()
                   .WithDestructurers([new DbUpdateExceptionDestructurer()]));

            var otlpExporterEndpoint = builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"];

            if (!string.IsNullOrWhiteSpace(otlpExporterEndpoint))
            {
                loggerConfiguration.WriteTo.OpenTelemetry(otlpExporterEndpoint);
            }
        });

        return builder;
    }

    public static WebApplication UseDefaultSerilog(this WebApplication app)
    {
        app.UseSerilogRequestLogging(options =>
        {
            options.IncludeQueryInRequestPath = true;
        });

        return app;
    }
}
