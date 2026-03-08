using Demo.Domain.AggregatesModel.ProductAggregate;

namespace Demo.Application.UseCases.Products.Commands.DiscontinueProduct;

public class DiscontinueProductCommandValidator : AbstractValidator<DiscontinueProductCommand>
{
    public DiscontinueProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithState(_ => ProductErrors.IdRequired());
    }
}
