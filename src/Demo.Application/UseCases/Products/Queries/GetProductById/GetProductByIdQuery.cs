using Demo.Application.Models;
using Demo.SharedKernel.Results;

namespace Demo.Application.UseCases.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid ProductId)
    : IQuery<Result<ProductDetailsResponse>>;
