using Demo.Application.Abstractions;
using Demo.Domain.AggregatesModel.ProductAggregate;
using Demo.SharedKernel.Results;

namespace Demo.Application.UseCases.Products.Commands.ActivateProduct;

public class ActivateProductCommandHandler(IApplicationDbContext context)
    : ICommandHandler<ActivateProductCommand, Result>
{
    public async ValueTask<Result> Handle(ActivateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await context.Products
            .FirstOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken);

        if (product == null)
        {
            return Result.NotFound(ProductErrors.NotFound(command.ProductId));
        }

        product.Activate();

        await context.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
