using Demo.Application.Models;
using Demo.SharedKernel.Pagination;
using Demo.SharedKernel.Results;

namespace Demo.Application.UseCases.Products.Queries.ListProducts;

public record ListProductsQuery(int PageNumber, int PageSize)
    : IQuery<Result<PagedList<ProductListItemResponse>>>;
