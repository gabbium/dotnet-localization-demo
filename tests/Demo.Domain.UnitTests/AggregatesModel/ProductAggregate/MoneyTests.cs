using Demo.Domain.AggregatesModel.ProductAggregate;

namespace Demo.Domain.UnitTests.AggregatesModel.ProductAggregate;

public class MoneyTests
{
    [Fact]
    public void Constructor_ShouldConvertCurrencyToUppercase_WhenCurrencyIsLowercase()
    {
        // Arrange
        var amount = 10m;
        var currency = "usd";

        // Act
        var money = new Money(amount, currency);

        // Assert
        money.Currency.ShouldBe("USD");
    }

    [Fact]
    public void Constructor_ShouldSetAmountCorrectly()
    {
        // Arrange
        var amount = 25m;
        var currency = "USD";

        // Act
        var money = new Money(amount, currency);

        // Assert
        money.Amount.ShouldBe(amount);
    }
}
