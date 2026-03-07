namespace Demo.Api.Models;

public record CreateProductRequest
{
    [Required(ErrorMessage = "Product name is required")]
    [MaxLength(200, ErrorMessage = "Product name cannot exceed 200 characters")]
    [Description("Name of the product.")]
    public string Name { get; init; } = string.Empty;

    [MaxLength(1000, ErrorMessage = "Product description cannot exceed 1000 characters")]
    [Description("Optional description providing additional details about the product.")]
    public string? Description { get; init; }

    [Required(ErrorMessage = "Price amount is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price amount must be greater than zero")]
    [Description("Product price amount.")]
    public decimal Amount { get; init; }

    [Required(ErrorMessage = "Currency is required")]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency must be a valid 3-letter ISO 4217 code")]
    [Description("ISO 4217 currency code for the product price (e.g., USD, BRL, EUR).")]
    public string Currency { get; init; } = string.Empty;
}
