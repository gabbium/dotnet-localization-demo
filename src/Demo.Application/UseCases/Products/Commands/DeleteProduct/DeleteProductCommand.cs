using Demo.SharedKernel.Results;

namespace Demo.Application.UseCases.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid ProductId) : ICommand<Result>;
