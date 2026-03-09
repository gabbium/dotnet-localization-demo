using Demo.Application.FunctionalTests.TestSupport;
using Demo.Application.FunctionalTests.TestSupport.Extensions;
using Demo.Application.UseCases.Products.Commands.ChangeProductPrice;
using Demo.Domain.AggregatesModel.ProductAggregate;
using Demo.SharedKernel.Results;

namespace Demo.Application.FunctionalTests.UseCases.Products.Commands;

public class ChangeProductPriceTests(ApplicationTestFixture fixture)
    : FunctionalTestBase(fixture)
{
    [Fact]
    public async Task ThrowsValidationException_WhenProductIdIsEmpty()
    {
        // Arrange
        var command = new ChangeProductPriceCommand(
            Guid.Empty,
            1000,
            "USD");

        // Act
        var exception = await Should.ThrowAsync<ValidationException>(
            () => SendAsync(command).AsTask());

        // Assert
        var error = exception.ShouldHaveSingleError();

        error.Code.ShouldBe(ProductErrors.IdRequired().Code);
        error.Parameters.ShouldBeEmpty();
    }

    [Fact]
    public async Task ThrowsValidationException_WhenAmountIsNotGreaterThanZero()
    {
        // Arrange
        var command = new ChangeProductPriceCommand(
            Guid.NewGuid(),
            0,
            "USD");

        // Act
        var exception = await Should.ThrowAsync<ValidationException>(
            () => SendAsync(command).AsTask());

        // Assert
        var error = exception.ShouldHaveSingleError();

        error.Code.ShouldBe(ProductErrors.PriceMustBeGreaterThan(0).Code);
        error.Parameters.ShouldBe([0]);
    }

    [Fact]
    public async Task ThrowsValidationException_WhenCurrencyIsEmpty()
    {
        // Arrange
        var command = new ChangeProductPriceCommand(
            Guid.NewGuid(),
            100,
            "");

        // Act
        var exception = await Should.ThrowAsync<ValidationException>(
            () => SendAsync(command).AsTask());

        // Assert
        var error = exception.ShouldHaveSingleError();

        error.Code.ShouldBe(ProductErrors.CurrencyRequired().Code);
        error.Parameters.ShouldBeEmpty();
    }

    [Fact]
    public async Task ThrowsValidationException_WhenCurrencyIsNotIsoCode()
    {
        // Arrange
        var command = new ChangeProductPriceCommand(
            Guid.NewGuid(),
            100,
            "US");

        // Act
        var exception = await Should.ThrowAsync<ValidationException>(
            () => SendAsync(command).AsTask());

        // Assert
        var error = exception.ShouldHaveSingleError();

        error.Code.ShouldBe(ProductErrors.CurrencyMustBeIsoCode().Code);
        error.Parameters.ShouldBeEmpty();
    }

    [Fact]
    public async Task ReturnsNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        var productId = Guid.NewGuid();

        var command = new ChangeProductPriceCommand(
            productId,
            1000,
            "USD");

        // Act
        var result = await SendAsync(command);

        // Assert
        result.Status.ShouldBe(ResultStatus.NotFound);

        var error = result.ShouldHaveSingleError();

        error.Code.ShouldBe(ProductErrors.NotFound(productId).Code);
        error.Parameters.ShouldBe([productId]);
    }

    [Fact]
    public async Task ChangesProductPrice_WhenProductExists()
    {
        // Arrange
        var product = new Product("Notebook", new Money(1000, "USD"));

        await AddAsync(product);

        var command = new ChangeProductPriceCommand(
            product.Id,
            2000,
            "USD");

        // Act
        var result = await SendAsync(command);

        // Assert
        result.Status.ShouldBe(ResultStatus.NoContent);

        var persisted = await FindAsync<Product>(product.Id);

        persisted.ShouldNotBeNull();
        persisted.Price.Amount.ShouldBe(2000);
        persisted.Price.Currency.ShouldBe("USD");
    }
}
