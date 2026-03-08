using Demo.SharedKernel.Results;

namespace Demo.Domain.AggregatesModel.ProductAggregate;

public static class ProductErrors
{
    public static Error NotFound(Guid id) =>
        new("Product.NotFound", id);

    public static Error IdRequired() =>
        new("Product.Id.Required");

    public static Error NameRequired() =>
        new("Product.Name.Required");

    public static Error NameTooLong(int maxLength) =>
        new("Product.Name.MaxLengthExceeded", maxLength);

    public static Error DescriptionTooLong(int maxLength) =>
        new("Product.Description.MaxLengthExceeded", maxLength);

    public static Error PriceMustBeGreaterThan(decimal min) =>
        new("Product.Price.MustBeGreaterThan", min);

    public static Error CurrencyRequired() =>
        new("Product.Currency.Required");

    public static Error CurrencyMustBeIsoCode() =>
        new("Product.Currency.MustBeIsoCode");

    public static Error ActivationNotAllowedForDiscontinued(Guid id) =>
        new("Product.Activation.NotAllowedForDiscontinued", id);
}
