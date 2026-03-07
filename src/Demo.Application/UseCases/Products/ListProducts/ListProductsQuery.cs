using Demo.Application.Models;
using Demo.SharedKernel.Results;

namespace Demo.Application.UseCases.Products.ListProducts;

public record ListProductsQuery(int PageNumber, int PageSize)
    : IQuery<Result<PagedList<ProductListItemResponse>>>;
