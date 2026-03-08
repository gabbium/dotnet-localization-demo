using Demo.Domain.AggregatesModel.ProductAggregate;
using Demo.SharedKernel.Exceptions;

namespace Demo.Domain.UnitTests.AggregatesModel.ProductAggregate;

public class ProductTests
{
    [Fact]
    public void Constructor_ShouldCreateProductWithDraftStatus()
    {
        // Arrange
        var name = "Notebook";
        var price = new Money(1000, "USD");

        // Act
        var product = new Product(name, price);

        // Assert
        product.Name.ShouldBe(name);
        product.Price.ShouldBe(price);
        product.Status.ShouldBe(ProductStatus.Draft);
    }

    [Fact]
    public void UpdateDetails_ShouldUpdateNameAndDescription_WhenProductIsNotDiscontinued()
    {
        // Arrange
        var product = new Product("A", new Money(10, "USD"));

        // Act
        product.UpdateDetails("B", "desc");

        // Assert
        product.Name.ShouldBe("B");
        product.Description.ShouldBe("desc");
    }

    [Fact]
    public void UpdateDetails_ShouldThrowDomainException_WhenProductIsDiscontinued()
    {
        // Arrange
        var product = new Product("A", new Money(10, "USD"));
        product.Activate();
        product.Discontinue();

        // Act
        var exception = Should.Throw<DomainException>(() => product.UpdateDetails("B", null));

        // Assert
        exception.Error.Code.ShouldBe(ProductErrors.ModificationNotAllowedForDiscontinued(product.Id).Code);
        exception.Error.Parameters.ShouldBe([product.Id]);
    }

    [Fact]
    public void ChangePrice_ShouldUpdatePrice_WhenProductIsNotDiscontinued()
    {
        // Arrange
        var product = new Product("A", new Money(10, "USD"));
        var newPrice = new Money(20, "USD");

        // Act
        product.ChangePrice(newPrice);

        // Assert
        product.Price.ShouldBe(newPrice);
    }

    [Fact]
    public void ChangePrice_ShouldThrowDomainException_WhenProductIsDiscontinued()
    {
        // Arrange
        var product = new Product("A", new Money(10, "USD"));
        product.Activate();
        product.Discontinue();

        // Act
        var exception = Should.Throw<DomainException>(() => product.ChangePrice(new Money(20, "USD")));

        // Assert
        exception.Error.Code.ShouldBe(ProductErrors.ModificationNotAllowedForDiscontinued(product.Id).Code);
        exception.Error.Parameters.ShouldBe([product.Id]);
    }

    [Fact]
    public void Activate_ShouldSetStatusToActive_WhenProductIsDraft()
    {
        // Arrange
        var product = new Product("A", new Money(10, "USD"));

        // Act
        product.Activate();

        // Assert
        product.Status.ShouldBe(ProductStatus.Active);
    }

    [Fact]
    public void Activate_ShouldDoNothing_WhenProductIsAlreadyActive()
    {
        // Arrange
        var product = new Product("A", new Money(10, "USD"));
        product.Activate();

        // Act
        product.Activate();

        // Assert
        product.Status.ShouldBe(ProductStatus.Active);
    }

    [Fact]
    public void Activate_ShouldThrowDomainException_WhenProductIsDiscontinued()
    {
        // Arrange
        var product = new Product("A", new Money(10, "USD"));
        product.Activate();
        product.Discontinue();

        // Act
        var exception = Should.Throw<DomainException>(product.Activate);

        // Assert
        exception.Error.Code.ShouldBe(ProductErrors.ActivationNotAllowedForDiscontinued(product.Id).Code);
        exception.Error.Parameters.ShouldBe([product.Id]);
    }

    [Fact]
    public void Discontinue_ShouldSetStatusToDiscontinued_WhenProductIsActive()
    {
        // Arrange
        var product = new Product("A", new Money(10, "USD"));
        product.Activate();

        // Act
        product.Discontinue();

        // Assert
        product.Status.ShouldBe(ProductStatus.Discontinued);
    }

    [Fact]
    public void Discontinue_ShouldThrowDomainException_WhenProductIsDraft()
    {
        // Arrange
        var product = new Product("A", new Money(10, "USD"));

        // Act
        var exception = Should.Throw<DomainException>(product.Discontinue);

        // Assert
        exception.Error.Code.ShouldBe(ProductErrors.DiscontinueNotAllowedForDraft(product.Id).Code);
        exception.Error.Parameters.ShouldBe([product.Id]);
    }

    [Fact]
    public void Discontinue_ShouldDoNothing_WhenProductIsAlreadyDiscontinued()
    {
        // Arrange
        var product = new Product("A", new Money(10, "USD"));
        product.Activate();
        product.Discontinue();

        // Act
        product.Discontinue();

        // Assert
        product.Status.ShouldBe(ProductStatus.Discontinued);
    }
}
