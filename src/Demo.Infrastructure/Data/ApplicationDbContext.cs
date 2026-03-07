using Demo.Application.Abstractions;
using Demo.Domain.AggregatesModel.ProductAggregate;
using Demo.Infrastructure.Data.Configurations;

namespace Demo.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<ProductStatus>();
        modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
    }
}
