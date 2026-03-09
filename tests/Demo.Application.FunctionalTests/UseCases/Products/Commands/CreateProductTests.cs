using Demo.Application.FunctionalTests.TestSupport;
using Demo.Application.FunctionalTests.TestSupport.Extensions;
using Demo.Application.UseCases.Products.Commands.CreateProduct;
using Demo.Domain.AggregatesModel.ProductAggregate;
using Demo.SharedKernel.Results;

namespace Demo.Application.FunctionalTests.UseCases.Products.Commands;

public class CreateProductTests(ApplicationTestFixture fixture)
    : FunctionalTestBase(fixture)
{
    [Fact]
    public async Task ThrowsValidationException_WhenNameIsEmpty()
    {
        // Arrange
        var command = new CreateProductCommand(
            "",
            "Description",
            100,
            "USD");

        // Act
        var exception = await Should.ThrowAsync<ValidationException>(
            () => SendAsync(command).AsTask());

        // Assert
        var error = exception.ShouldHaveSingleError();

        error.Code.ShouldBe(ProductErrors.NameRequired().Code);
        error.Parameters.ShouldBeEmpty();
    }

    [Fact]
    public async Task ThrowsValidationException_WhenNameIsTooLong()
    {
        // Arrange
        var command = new CreateProductCommand(
            new string('A', 201),
            "Description",
            100,
            "USD");

        // Act
        var exception = await Should.ThrowAsync<ValidationException>(
            () => SendAsync(command).AsTask());

        // Assert
        var error = exception.ShouldHaveSingleError();

        error.Code.ShouldBe(ProductErrors.NameTooLong(200).Code);
        error.Parameters.ShouldBe([200]);
    }

    [Fact]
    public async Task ThrowsValidationException_WhenDescriptionIsTooLong()
    {
        // Arrange
        var command = new CreateProductCommand(
            "Notebook",
            new string('A', 1001),
            100,
            "USD");

        // Act
        var exception = await Should.ThrowAsync<ValidationException>(
            () => SendAsync(command).AsTask());

        // Assert
        var error = exception.ShouldHaveSingleError();

        error.Code.ShouldBe(ProductErrors.DescriptionTooLong(1000).Code);
        error.Parameters.ShouldBe([1000]);
    }

    [Fact]
    public async Task ThrowsValidationException_WhenAmountIsInvalid()
    {
        // Arrange
        var command = new CreateProductCommand(
            "Notebook",
            "Description",
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
        var command = new CreateProductCommand(
            "Notebook",
            "Description",
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
    public async Task ThrowsValidationException_WhenCurrencyIsInvalid()
    {
        // Arrange
        var command = new CreateProductCommand(
            "Notebook",
            "Description",
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
    public async Task ReturnsCreatedProduct_WhenCommandIsValid()
    {
        // Arrange
        var command = new CreateProductCommand(
            "Notebook",
            "Gaming notebook",
            1000,
            "USD");

        // Act
        var result = await SendAsync(command);

        // Assert
        result.Status.ShouldBe(ResultStatus.Created);

        result.Value.ShouldNotBeNull();
        result.Value.Id.ShouldNotBe(Guid.Empty);
        result.Value.Name.ShouldBe(command.Name);
        result.Value.Description.ShouldBe(command.Description);
        result.Value.Amount.ShouldBe(command.Amount);
        result.Value.Currency.ShouldBe(command.Currency);
        result.Value.Status.ShouldBe(ProductStatus.Draft);
        result.Value.CreatedAt.ShouldBe(DateTime.UtcNow, TimeSpan.FromSeconds(5));

        var persisted = await FindAsync<Product>(result.Value.Id);

        persisted.ShouldNotBeNull();
        persisted.Name.ShouldBe(command.Name);
        persisted.Description.ShouldBe(command.Description);
        persisted.Price.Amount.ShouldBe(command.Amount);
        persisted.Price.Currency.ShouldBe(command.Currency);
        persisted.Status.ShouldBe(ProductStatus.Draft);
    }
}
