namespace Demo.Application.Models;

public record PaginatedList<T>
{
    [Description("List of items in the current page.")]
    public IReadOnlyList<T> Items { get; }

    [Description("Current page number (1-based).")]
    public int PageNumber { get; }

    [Description("Number of items per page.")]
    public int PageSize { get; }

    [Description("Total number of items across all pages.")]
    public int TotalItems { get; }

    [Description("Total number of pages.")]
    public int TotalPages { get; }

    [Description("Indicates whether there is a previous page.")]
    public bool HasPreviousPage => PageNumber > 1;

    [Description("Indicates whether there is a next page.")]
    public bool HasNextPage => PageNumber < TotalPages;

    public PaginatedList(IReadOnlyList<T> items, int totalItems, int pageNumber, int pageSize)
    {
        Items = items;
        TotalItems = totalItems;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
    }
}
