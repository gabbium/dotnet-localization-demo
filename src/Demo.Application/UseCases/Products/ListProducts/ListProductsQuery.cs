using Demo.Application.Models;

namespace Demo.Application.UseCases.Products.ListProducts;

public record ListProductsQuery(int PageNumber, int PageSize)
    : IQuery<Result<PaginatedList<ProductListItemResponse>>>;
