using Demo.SharedKernel.Results;

namespace Demo.Application.UseCases.Products.Commands.UpdateProductDetails;

public sealed record UpdateProductDetailsCommand(
    Guid ProductId,
    string Name,
    string? Description)
    : ICommand<Result>;
