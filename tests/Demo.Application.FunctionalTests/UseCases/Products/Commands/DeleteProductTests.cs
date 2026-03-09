using Demo.Application.FunctionalTests.TestSupport;
using Demo.Application.FunctionalTests.TestSupport.Extensions;
using Demo.Application.UseCases.Products.Commands.DeleteProduct;
using Demo.Domain.AggregatesModel.ProductAggregate;
using Demo.SharedKernel.Results;

namespace Demo.Application.FunctionalTests.UseCases.Products.Commands;

public class DeleteProductTests(ApplicationTestFixture fixture)
    : FunctionalTestBase(fixture)
{
    [Fact]
    public async Task ThrowsValidationException_WhenProductIdIsEmpty()
    {
        // Arrange
        var command = new DeleteProductCommand(Guid.Empty);

        // Act
        var exception = await Should.ThrowAsync<ValidationException>(
            () => SendAsync(command).AsTask());

        // Assert
        var error = exception.ShouldHaveSingleError();

        error.Code.ShouldBe(ProductErrors.IdRequired().Code);
        error.Parameters.ShouldBeEmpty();
    }

    [Fact]
    public async Task ReturnsNoContent_WhenProductDoesNotExist()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var command = new DeleteProductCommand(productId);

        // Act
        var result = await SendAsync(command);

        // Assert
        result.Status.ShouldBe(ResultStatus.NoContent);
    }

    [Fact]
    public async Task ReturnsInvalid_WhenProductIsNotDraft()
    {
        // Arrange
        var product = new Product("Notebook", new Money(1000, "USD"));
        product.Activate();

        await AddAsync(product);

        var command = new DeleteProductCommand(product.Id);

        // Act
        var result = await SendAsync(command);

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);

        var error = result.ShouldHaveSingleError();

        error.Code.ShouldBe(ProductErrors.DeleteNotAllowedForNonDraft(product.Id).Code);
        error.Parameters.ShouldBe([product.Id]);
    }

    [Fact]
    public async Task DeletesProduct_WhenProductIsDraft()
    {
        // Arrange
        var product = new Product("Notebook", new Money(1000, "USD"));

        await AddAsync(product);

        var command = new DeleteProductCommand(product.Id);

        // Act
        var result = await SendAsync(command);

        // Assert
        result.Status.ShouldBe(ResultStatus.NoContent);

        var persisted = await FindAsync<Product>(product.Id);

        persisted.ShouldBeNull();
    }
}
