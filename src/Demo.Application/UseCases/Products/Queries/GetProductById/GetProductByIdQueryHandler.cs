using Demo.Application.Abstractions;
using Demo.Application.Models;
using Demo.Domain.AggregatesModel.ProductAggregate;
using Demo.SharedKernel.Results;

namespace Demo.Application.UseCases.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetProductByIdQuery, Result<ProductDetailsResponse>>
{
    public async ValueTask<Result<ProductDetailsResponse>> Handle(
        GetProductByIdQuery query,
        CancellationToken cancellationToken)
    {
        var product = await context.Products
            .AsNoTracking()
            .Where(p => p.Id == query.ProductId)
            .Select(p => new ProductDetailsResponse()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Amount = p.Price.Amount,
                Currency = p.Price.Currency,
                Status = p.Status,
                CreatedAt = p.CreatedAt,
                LastModifiedAt = p.LastModifiedAt
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (product == null)
        {
            return Result.NotFound(ProductErrors.NotFound(query.ProductId));
        }

        return product;
    }
}
