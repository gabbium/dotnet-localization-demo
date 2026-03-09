using Demo.Application.FunctionalTests.TestSupport;
using Demo.Application.FunctionalTests.TestSupport.Extensions;
using Demo.Application.UseCases.Products.Commands.DiscontinueProduct;
using Demo.Domain.AggregatesModel.ProductAggregate;
using Demo.SharedKernel.Results;

namespace Demo.Application.FunctionalTests.UseCases.Products.Commands;

public class DiscontinueProductTests(ApplicationTestFixture fixture)
    : FunctionalTestBase(fixture)
{
    [Fact]
    public async Task ThrowsValidationException_WhenProductIdIsEmpty()
    {
        // Arrange
        var command = new DiscontinueProductCommand(Guid.Empty);

        // Act
        var exception = await Should.ThrowAsync<ValidationException>(
            () => SendAsync(command).AsTask());

        // Assert
        var error = exception.ShouldHaveSingleError();

        error.Code.ShouldBe(ProductErrors.IdRequired().Code);
        error.Parameters.ShouldBeEmpty();
    }

    [Fact]
    public async Task ReturnsNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var command = new DiscontinueProductCommand(productId);

        // Act
        var result = await SendAsync(command);

        // Assert
        result.Status.ShouldBe(ResultStatus.NotFound);

        var error = result.ShouldHaveSingleError();

        error.Code.ShouldBe(ProductErrors.NotFound(productId).Code);
        error.Parameters.ShouldBe([productId]);
    }

    [Fact]
    public async Task DiscontinuesProduct_WhenProductIsActive()
    {
        // Arrange
        var product = new Product("Notebook", new Money(1000, "USD"));

        product.Activate();

        await AddAsync(product);

        var command = new DiscontinueProductCommand(product.Id);

        // Act
        var result = await SendAsync(command);

        // Assert
        result.Status.ShouldBe(ResultStatus.NoContent);

        var persisted = await FindAsync<Product>(product.Id);

        persisted.ShouldNotBeNull();
        persisted.Status.ShouldBe(ProductStatus.Discontinued);
    }
}
