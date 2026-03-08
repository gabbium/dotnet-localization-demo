using Demo.Application.Abstractions;
using Demo.Domain.AggregatesModel.ProductAggregate;
using Demo.SharedKernel.Results;

namespace Demo.Application.UseCases.Products.Commands.ChangeProductPrice;

public class ChangeProductPriceCommandHandler(IApplicationDbContext context)
    : ICommandHandler<ChangeProductPriceCommand, Result>
{
    public async ValueTask<Result> Handle(
        ChangeProductPriceCommand command,
        CancellationToken cancellationToken)
    {
        var product = await context.Products
            .FirstOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken);

        if (product == null)
        {
            return Result.NotFound(ProductErrors.NotFound(command.ProductId));
        }

        var price = new Money(command.Amount, command.Currency);

        product.ChangePrice(price);

        await context.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
