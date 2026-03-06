namespace Demo.Domain.AggregatesModel.ProductAggregate;

public class Money(decimal amount, string currency)
{
    public decimal Amount { get; } = amount;
    public string Currency { get; } = currency.ToUpperInvariant();
}
