using Demo.Application.FunctionalTests.TestSupport;
using Demo.Application.FunctionalTests.TestSupport.Extensions;
using Demo.Application.UseCases.Products.Queries.ListProducts;
using Demo.Domain.AggregatesModel.ProductAggregate;
using Demo.SharedKernel.Pagination;
using Demo.SharedKernel.Results;

namespace Demo.Application.FunctionalTests.UseCases.Products.Queries;

public class ListProductsTests(ApplicationTestFixture fixture)
    : FunctionalTestBase(fixture)
{
    [Fact]
    public async Task ThrowsValidationException_WhenPageNumberIsInvalid()
    {
        // Arrange
        var query = new ListProductsQuery(0, 10);

        // Act
        var exception = await Should.ThrowAsync<ValidationException>(
            () => SendAsync(query).AsTask());

        // Assert
        var error = exception.ShouldHaveSingleError();

        error.Code.ShouldBe(PaginationErrors.PageNumberMustBeGreaterThanOrEqualTo(1).Code);
        error.Parameters.ShouldBe([1]);
    }

    [Fact]
    public async Task ThrowsValidationException_WhenPageSizeIsInvalid()
    {
        // Arrange
        var query = new ListProductsQuery(1, 101);

        // Act
        var exception = await Should.ThrowAsync<ValidationException>(
            () => SendAsync(query).AsTask());

        // Assert
        var error = exception.ShouldHaveSingleError();

        error.Code.ShouldBe(PaginationErrors.PageSizeMustBeBetween(1, 100).Code);
        error.Parameters.ShouldBe([1, 100]);
    }

    [Fact]
    public async Task ReturnsEmptyList_WhenNoProductsExist()
    {
        // Arrange
        var query = new ListProductsQuery(1, 10);

        // Act
        var result = await SendAsync(query);

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);

        result.Value.ShouldNotBeNull();
        result.Value.Items.ShouldBeEmpty();
        result.Value.TotalItems.ShouldBe(0);
        result.Value.PageNumber.ShouldBe(1);
        result.Value.PageSize.ShouldBe(10);
    }

    [Fact]
    public async Task ReturnsProducts_WhenProductsExist()
    {
        // Arrange
        var product1 = new Product("Notebook", new Money(1000, "USD"));
        var product2 = new Product("Mouse", new Money(50, "USD"));

        await AddAsync(product1);
        await AddAsync(product2);

        var query = new ListProductsQuery(1, 10);

        // Act
        var result = await SendAsync(query);

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);

        result.Value.ShouldNotBeNull();
        result.Value.Items.Count.ShouldBe(2);
        result.Value.TotalItems.ShouldBe(2);

        var item = result.Value.Items.First(x => x.Id == product1.Id);

        item.Id.ShouldBe(product1.Id);
        item.Name.ShouldBe(product1.Name);
        item.Amount.ShouldBe(product1.Price.Amount);
        item.Currency.ShouldBe(product1.Price.Currency);
        item.Status.ShouldBe(product1.Status);
    }
}
