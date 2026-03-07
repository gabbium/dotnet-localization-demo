namespace Demo.Api.Models;

/// <summary>
/// Request used to create a new product.
/// </summary>
public sealed record CreateProductRequest
{
    /// <summary>
    /// Name of the product.
    /// Maximum length: 200 characters.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Optional description providing additional details about the product.
    /// Maximum length: 1000 characters.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Product price amount.
    /// Must be greater than zero.
    /// </summary>
    public decimal Amount { get; init; }

    /// <summary>
    /// ISO 4217 currency code for the product price (e.g., USD, BRL, EUR).
    /// Must contain exactly 3 characters.
    /// </summary>
    public string Currency { get; init; } = string.Empty;
}
