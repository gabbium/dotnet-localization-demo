using Demo.Domain.AggregatesModel.ProductAggregate;

namespace Demo.Application.Models;

public record ProductListItemResponse
{
    [Description("Unique identifier of the product.")]
    public Guid Id { get; init; }

    [Description("Name of the product.")]
    public string Name { get; init; } = null!;

    [Description("Product price amount.")]
    public decimal Amount { get; init; }

    [Description("ISO 4217 currency code of the product price.")]
    public string Currency { get; init; } = null!;

    [Description("Current lifecycle status of the product.")]
    public ProductStatus Status { get; init; }
}
