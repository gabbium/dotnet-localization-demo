using Demo.Application.Models;
using Demo.SharedKernel.Results;

namespace Demo.Application.UseCases.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    string? Description,
    decimal Amount,
    string Currency)
    : ICommand<Result<ProductDetailsResponse>>;
