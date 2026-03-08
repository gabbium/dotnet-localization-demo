using Demo.Domain.AggregatesModel.ProductAggregate;

namespace Demo.Application.UseCases.Products.Commands.ChangeProductPrice;

public class ChangeProductPriceCommandValidator : AbstractValidator<ChangeProductPriceCommand>
{
    public ChangeProductPriceCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithState(_ => ProductErrors.IdRequired());

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithState(_ => ProductErrors.PriceMustBeGreaterThan(0));

        RuleFor(x => x.Currency)
            .NotEmpty()
                .WithState(_ => ProductErrors.CurrencyRequired())
            .Length(3)
                .WithState(_ => ProductErrors.CurrencyMustBeIsoCode());
    }
}
