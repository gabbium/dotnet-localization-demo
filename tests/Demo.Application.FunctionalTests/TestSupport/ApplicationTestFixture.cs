using Demo.Application.FunctionalTests.TestSupport.Containers;
using Demo.Infrastructure;

namespace Demo.Application.FunctionalTests.TestSupport;

public class ApplicationTestFixture : IAsyncLifetime
{
    public PostgresContainer Db { get; } = new();
    public IServiceProvider ServiceProvider { get; private set; } = default!;

    public async Task InitializeAsync()
    {
        await Db.InitializeAsync();

        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:DemoDb"] = Db.ConnectionString
            }).Build();

        services.AddLogging();
        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);

        ServiceProvider = services.BuildServiceProvider();

        await Db.InitializeDatabaseAsync(ServiceProvider);
    }

    public async Task ResetAsync() => await Db.ResetAsync();

    public async Task DisposeAsync() => await Db.DisposeAsync();
}
