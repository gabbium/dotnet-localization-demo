using Demo.Domain.AggregatesModel.ProductAggregate;

namespace Demo.Application.Models;

public record ProductDetailsResponse
{
    [Description("Unique identifier of the product.")]
    public Guid Id { get; init; }

    [Description("Name of the product.")]
    public string Name { get; init; } = null!;

    [Description("Optional detailed description of the product.")]
    public string? Description { get; init; }

    [Description("Product price amount.")]
    public decimal Amount { get; init; }

    [Description("ISO 4217 currency code of the product price.")]
    public string Currency { get; init; } = null!;

    [Description("Current lifecycle status of the product.")]
    public ProductStatus Status { get; init; }

    [Description("Date and time when the product was created (UTC).")]
    public DateTimeOffset CreatedAt { get; init; }

    [Description("Date and time when the product was last modified (UTC), if applicable.")]
    public DateTimeOffset? LastModifiedAt { get; init; }
}
