using Demo.Domain.AggregatesModel.ProductAggregate;

namespace Demo.Application.UseCases.Products.Commands.CreateProduct;

/// <summary>
/// Represents detailed information about a product.
/// </summary>
public record ProductDetailsResponse
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
    /// Optional detailed description of the product.
    /// </summary>
    public string? Description { get; init; }

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

    /// <summary>
    /// Date and time when the product was created (UTC).
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Date and time when the product was last modified (UTC), if applicable.
    /// </summary>
    public DateTimeOffset? LastModifiedAt { get; init; }
}
