using Demo.Application.Interfaces;
using Demo.Application.Models;

namespace Demo.Application.UseCases.Products.ListProducts;

public class ListProductsQueryHandler(IApplicationDbContext context)
    : IQueryHandler<ListProductsQuery, Result<PaginatedList<ProductListItemResponse>>>
{
    public async ValueTask<Result<PaginatedList<ProductListItemResponse>>> Handle(
        ListProductsQuery query,
        CancellationToken cancellationToken)
    {
        var baseQuery = context.Products.AsNoTracking();

        var totalItems = await baseQuery.CountAsync(cancellationToken);

        var items = await baseQuery
            .OrderByDescending(p => p.CreatedAt)
            .ThenBy(p => p.Id)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(p => new ProductListItemResponse()
            {
                Id = p.Id,
                Name = p.Name,
                Amount = p.Price.Amount,
                Currency = p.Price.Currency,
                Status = p.Status
            })
            .ToListAsync(cancellationToken);

        return new PaginatedList<ProductListItemResponse>(
            items,
            totalItems,
            query.PageNumber,
            query.PageSize);
    }
}
