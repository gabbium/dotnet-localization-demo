namespace Demo.Api.Models;

/// <summary>
/// Request parameters for retrieving a paginated list of products.
/// </summary>
public record ListProductsRequest
{
    /// <summary>
    /// Current page number (1-based).
    /// Default value: 1.
    /// Minimum value: 1.
    /// </summary>
    public int PageNumber { get; init; } = 1;

    /// <summary>
    /// Number of items per page.
    /// Default value: 20.
    /// Allowed range: 1 to 100.
    /// </summary>
    public int PageSize { get; init; } = 20;
}
