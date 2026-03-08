using Demo.Application.Abstractions;
using Demo.Domain.AggregatesModel.ProductAggregate;
using Demo.SharedKernel.Results;

namespace Demo.Application.UseCases.Products.Commands.UpdateProductDetails;

public class UpdateProductDetailsCommandHandler(IApplicationDbContext context)
    : ICommandHandler<UpdateProductDetailsCommand, Result>
{
    public async ValueTask<Result> Handle(UpdateProductDetailsCommand command, CancellationToken cancellationToken)
    {
        var product = await context.Products
            .FirstOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken);

        if (product == null)
        {
            return Result.NotFound(ProductErrors.NotFound(command.ProductId));
        }

        product.UpdateDetails(command.Name, command.Description);

        await context.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
