namespace Demo.Api.Models;

public record ListProductsRequest
{
    [Required]
    [Range(1, int.MaxValue)]
    [DefaultValue(1)]
    [Description("Current page number (1-based).")]
    public int PageNumber { get; init; }

    [Required]
    [Range(1, 100)]
    [DefaultValue(20)]
    [Description("Number of items per page.")]
    public int PageSize { get; init; }
}
