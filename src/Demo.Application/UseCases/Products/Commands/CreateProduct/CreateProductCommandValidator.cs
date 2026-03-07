using Demo.Domain.AggregatesModel.ProductAggregate;

namespace Demo.Application.UseCases.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithState(_ => ProductErrors.NameRequired())
            .MaximumLength(200)
            .WithState(_ => ProductErrors.NameTooLong(200));

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .WithState(_ => ProductErrors.DescriptionTooLong(1000));

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
