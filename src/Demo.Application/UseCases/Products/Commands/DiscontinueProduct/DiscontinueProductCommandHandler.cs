using Demo.Application.Abstractions;
using Demo.Domain.AggregatesModel.ProductAggregate;
using Demo.SharedKernel.Results;

namespace Demo.Application.UseCases.Products.Commands.DiscontinueProduct;

public class DiscontinueProductCommandHandler(IApplicationDbContext context)
    : ICommandHandler<DiscontinueProductCommand, Result>
{
    public async ValueTask<Result> Handle(DiscontinueProductCommand command, CancellationToken cancellationToken)
    {
        var product = await context.Products
            .FirstOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken);

        if (product == null)
        {
            return Result.NotFound(ProductErrors.NotFound(command.ProductId));
        }

        product.Discontinue();

        await context.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
