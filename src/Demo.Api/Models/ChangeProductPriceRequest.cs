namespace Demo.Api.Models;

/// <summary>
/// Request used to change the price of an existing product.
/// </summary>
public sealed record ChangeProductPriceRequest
{
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
