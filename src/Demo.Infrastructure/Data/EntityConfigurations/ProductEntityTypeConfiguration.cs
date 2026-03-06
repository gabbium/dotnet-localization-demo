using Demo.Domain.AggregatesModel.ProductAggregate;

namespace Demo.Infrastructure.Data.EntityConfigurations;

public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Description)
            .HasMaxLength(1000);

        builder.Property(p => p.Status)
            .IsRequired();

        builder.OwnsOne(p => p.Price, price =>
        {
            price.Property(p => p.Amount)
                .HasPrecision(18, 2)
                .IsRequired();

            price.Property(p => p.Currency)
                .HasMaxLength(3)
                .IsRequired();
        });

        builder.Navigation(p => p.Price)
            .IsRequired();
    }
}
