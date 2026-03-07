using Demo.Domain.AggregatesModel.ProductAggregate;

namespace Demo.Application.Models;

/// <summary>
/// Represents a product item in a product list.
/// </summary>
public record ProductListItemResponse
{
    /// <summary>
    /// Unique identifier of the product.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Name of the product.
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// Product price amount.
    /// </summary>
    public decimal Amount { get; init; }

    /// <summary>
    /// ISO 4217 currency code of the product price.
    /// </summary>
    public string Currency { get; init; } = null!;

    /// <summary>
    /// Current lifecycle status of the product.
    /// </summary>
    public ProductStatus Status { get; init; }
}
