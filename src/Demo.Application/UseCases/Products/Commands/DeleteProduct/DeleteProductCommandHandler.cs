using Demo.Application.Abstractions;
using Demo.Domain.AggregatesModel.ProductAggregate;
using Demo.SharedKernel.Results;

namespace Demo.Application.UseCases.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler(IApplicationDbContext context)
    : ICommandHandler<DeleteProductCommand, Result>
{
    public async ValueTask<Result> Handle(
        DeleteProductCommand command,
        CancellationToken cancellationToken)
    {
        var product = await context.Products
            .FirstOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken);

        if (product == null)
        {
            return Result.NotFound(ProductErrors.NotFound(command.ProductId));
        }

        if (product.Status != ProductStatus.Draft)
        {
            return Result.Invalid(ProductErrors.DeleteNotAllowedForNonDraft(command.ProductId));
        }

        context.Products.Remove(product);

        await context.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
