namespace Demo.Api.Models;

/// <summary>
/// Request used to update product details.
/// </summary>
public sealed record UpdateProductRequest
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
}
