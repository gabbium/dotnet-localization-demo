using Demo.Application.Interfaces;
using Demo.Application.Models;
using Demo.Domain.AggregatesModel.ProductAggregate;

namespace Demo.Application.UseCases.Products.CreateProduct;

public class CreateProductCommandHandler(IApplicationDbContext context)
    : ICommandHandler<CreateProductCommand, Result<ProductDetailsResponse>>
{
    public async ValueTask<Result<ProductDetailsResponse>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var price = new Money(command.Amount, command.Currency);

        var product = new Product(
            command.Name,
            price,
            command.Description);

        await context.Products.AddAsync(product, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return new ProductDetailsResponse()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Amount = product.Price.Amount,
            Currency = product.Price.Currency,
            Status = product.Status,
            CreatedAt = product.CreatedAt,
            LastModifiedAt = product.LastModifiedAt
        };
    }
}
