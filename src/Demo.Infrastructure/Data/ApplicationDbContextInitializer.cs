using Demo.Domain.AggregatesModel.ProductAggregate;

namespace Demo.Infrastructure.Data;

public class ApplicationDbContextInitializer(
    ILogger<ApplicationDbContextInitializer> logger,
    ApplicationDbContext context)
{
    public async Task InitializeAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
            await SeedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initializing the database");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        if (await context.Products.AnyAsync())
        {
            return;
        }

        var products = new List<Product>
        {
            new("Dell XPS 13 Laptop", new(9500m, "USD"), "Premium laptop for development"),
            new("LG 27'' 4K Monitor", new(2200m, "USD"), "4K IPS monitor for productivity"),
            new("Logitech MX Master 3S Mouse", new(550m, "USD"), "Ergonomic mouse designed for productivity"),
            new("Teclado Mecânico Keychron K8", new(650m, "BRL"), "Teclado mecânico sem fio para programação"),
            new("Auriculares Sony WH-1000XM5", new(1800m, "EUR"), "Auriculares inalámbricos con cancelación de ruido")
        };

        await context.Products.AddRangeAsync(products);

        await context.SaveChangesAsync();
    }
}

public static class InitializerExtensions
{
    public static async Task InitializeDatabaseAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
        await initialiser.InitializeAsync();
    }
}
