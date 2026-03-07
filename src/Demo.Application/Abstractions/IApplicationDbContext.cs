using Demo.Domain.AggregatesModel.ProductAggregate;

namespace Demo.Application.Abstractions;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
