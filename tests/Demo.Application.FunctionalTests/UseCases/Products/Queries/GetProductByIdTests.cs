using Demo.Application.FunctionalTests.TestSupport;
using Demo.Application.FunctionalTests.TestSupport.Extensions;
using Demo.Application.UseCases.Products.Queries.GetProductById;
using Demo.Domain.AggregatesModel.ProductAggregate;
using Demo.SharedKernel.Results;

namespace Demo.Application.FunctionalTests.UseCases.Products.Queries;

public class GetProductByIdTests(ApplicationTestFixture fixture)
    : FunctionalTestBase(fixture)
{
    [Fact]
    public async Task ThrowsValidationException_WhenProductIdIsEmpty()
    {
        // Arrange
        var query = new GetProductByIdQuery(Guid.Empty);

        // Act
        var exception = await Should.ThrowAsync<ValidationException>(
            () => SendAsync(query).AsTask());

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
        var query = new GetProductByIdQuery(productId);

        // Act
        var result = await SendAsync(query);

        // Assert
        result.Status.ShouldBe(ResultStatus.NotFound);

        var error = result.ShouldHaveSingleError();

        error.Code.ShouldBe(ProductErrors.NotFound(productId).Code);
        error.Parameters.ShouldBe([productId]);
    }

    [Fact]
    public async Task ReturnsProduct_WhenProductExists()
    {
        // Arrange
        var product = new Product("Notebook", new Money(1000, "USD"));

        await AddAsync(product);

        var query = new GetProductByIdQuery(product.Id);

        // Act
        var result = await SendAsync(query);

        // Assert
        product.LastModifiedAt.ShouldNotBeNull();

        result.Status.ShouldBe(ResultStatus.Ok);

        result.Value.ShouldNotBeNull();
        result.Value.Id.ShouldBe(product.Id);
        result.Value.Name.ShouldBe(product.Name);
        result.Value.Description.ShouldBe(product.Description);
        result.Value.Amount.ShouldBe(product.Price.Amount);
        result.Value.Currency.ShouldBe(product.Price.Currency);
        result.Value.Status.ShouldBe(product.Status);
        result.Value.CreatedAt.ShouldBe(product.CreatedAt, TimeSpan.FromSeconds(1));
        result.Value.LastModifiedAt.ShouldNotBeNull();
        result.Value.LastModifiedAt.Value.ShouldBe(product.LastModifiedAt.Value, TimeSpan.FromSeconds(1));
    }
}
