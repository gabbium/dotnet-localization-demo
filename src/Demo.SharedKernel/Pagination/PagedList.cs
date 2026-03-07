namespace Demo.SharedKernel.Pagination;

/// <summary>
/// Represents a paginated list of items.
/// </summary>
public record PagedList<T>
{
    /// <summary>
    /// List of items in the current page.
    /// </summary>
    public IReadOnlyList<T> Items { get; }

    /// <summary>
    /// Current page number (1-based).
    /// </summary>
    public int PageNumber { get; }

    /// <summary>
    /// Number of items per page.
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    /// Total number of items across all pages.
    /// </summary>
    public int TotalItems { get; }

    /// <summary>
    /// Total number of pages.
    /// </summary>
    public int TotalPages { get; }

    /// <summary>
    /// Indicates whether there is a previous page.
    /// </summary>
    public bool HasPreviousPage => PageNumber > 1;

    /// <summary>
    /// Indicates whether there is a next page.
    /// </summary>
    public bool HasNextPage => PageNumber < TotalPages;

    /// <summary>
    /// Initializes a new instance of a paginated list.
    /// </summary>
    /// <param name="items">Items in the current page.</param>
    /// <param name="totalItems">Total number of items across all pages.</param>
    /// <param name="pageNumber">Current page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    public PagedList(IReadOnlyList<T> items, int totalItems, int pageNumber, int pageSize)
    {
        Items = items;
        TotalItems = totalItems;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
    }
}
