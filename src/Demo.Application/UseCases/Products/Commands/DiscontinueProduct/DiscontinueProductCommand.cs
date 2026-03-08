using Demo.SharedKernel.Results;

namespace Demo.Application.UseCases.Products.Commands.DiscontinueProduct;

public record DiscontinueProductCommand(Guid ProductId) : ICommand<Result>;
