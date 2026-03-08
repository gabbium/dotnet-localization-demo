using Demo.SharedKernel.Results;

namespace Demo.Application.UseCases.Products.Commands.ChangeProductPrice;

public record ChangeProductPriceCommand(
    Guid ProductId,
    decimal Amount,
    string Currency)
    : ICommand<Result>;
