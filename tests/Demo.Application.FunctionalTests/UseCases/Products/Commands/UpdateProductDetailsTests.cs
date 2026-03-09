using Demo.Application.FunctionalTests.TestSupport;
using Demo.Application.FunctionalTests.TestSupport.Extensions;
using Demo.Application.UseCases.Products.Commands.UpdateProductDetails;
using Demo.Domain.AggregatesModel.ProductAggregate;
using Demo.SharedKernel.Results;

namespace Demo.Application.FunctionalTests.UseCases.Products.Commands;

public class UpdateProductDetailsTests(ApplicationTestFixture fixture)
    : FunctionalTestBase(fixture)
{
    [Fact]
    public async Task ThrowsValidationException_WhenProductIdIsEmpty()
    {
        // Arrange
        var command = new UpdateProductDetailsCommand(
            Guid.Empty,
            "Notebook",
            "Description");

        // Act
        var exception = await Should.ThrowAsync<ValidationException>(
            () => SendAsync(command).AsTask());

        // Assert
        var error = exception.ShouldHaveSingleError();

        error.Code.ShouldBe(ProductErrors.IdRequired().Code);
        error.Parameters.ShouldBeEmpty();
    }

    [Fact]
    public async Task ThrowsValidationException_WhenNameIsEmpty()
    {
        // Arrange
        var command = new UpdateProductDetailsCommand(
            Guid.NewGuid(),
            "",
            "Description");

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
        var name = new string('A', 201);

        var command = new UpdateProductDetailsCommand(
            Guid.NewGuid(),
            name,
            "Description");

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
        var description = new string('A', 1001);

        var command = new UpdateProductDetailsCommand(
            Guid.NewGuid(),
            "Notebook",
            description);

        // Act
        var exception = await Should.ThrowAsync<ValidationException>(
            () => SendAsync(command).AsTask());

        // Assert
        var error = exception.ShouldHaveSingleError();

        error.Code.ShouldBe(ProductErrors.DescriptionTooLong(1000).Code);
        error.Parameters.ShouldBe([1000]);
    }

    [Fact]
    public async Task ReturnsNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        var productId = Guid.NewGuid();

        var command = new UpdateProductDetailsCommand(
            productId,
            "Notebook",
            "Description");

        // Act
        var result = await SendAsync(command);

        // Assert
        result.Status.ShouldBe(ResultStatus.NotFound);

        var error = result.ShouldHaveSingleError();

        error.Code.ShouldBe(ProductErrors.NotFound(productId).Code);
        error.Parameters.ShouldBe([productId]);
    }

    [Fact]
    public async Task UpdatesProductDetails_WhenProductExists()
    {
        // Arrange
        var product = new Product("Old Name", new Money(1000, "USD"), "Old Description");

        await AddAsync(product);

        var command = new UpdateProductDetailsCommand(
            product.Id,
            "New Name",
            "New Description");

        // Act
        var result = await SendAsync(command);

        // Assert
        result.Status.ShouldBe(ResultStatus.NoContent);

        var persisted = await FindAsync<Product>(product.Id);

        persisted.ShouldNotBeNull();
        persisted.Name.ShouldBe("New Name");
        persisted.Description.ShouldBe("New Description");
    }
}
