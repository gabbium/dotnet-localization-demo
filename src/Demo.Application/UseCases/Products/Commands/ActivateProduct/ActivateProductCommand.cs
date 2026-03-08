using Demo.SharedKernel.Results;

namespace Demo.Application.UseCases.Products.Commands.ActivateProduct;

public record ActivateProductCommand(Guid ProductId) : ICommand<Result>;
