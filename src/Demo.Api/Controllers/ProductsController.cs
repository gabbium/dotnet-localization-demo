using Demo.Api.Models;
using Demo.Api.Resources;
using Demo.Application.Models;
using Demo.Application.UseCases.Products.Commands.CreateProduct;
using Demo.Application.UseCases.Products.Commands.DeleteProduct;
using Demo.Application.UseCases.Products.Queries.GetProductById;
using Demo.Application.UseCases.Products.Queries.ListProducts;
using Demo.SharedKernel.Pagination;

namespace Demo.Api.Controllers;

public class ProductsController(
    IMediator mediator,
    IStringLocalizer<SharedResource> localizer) : BaseController(localizer)
{
    /// <summary>
    /// Retrieve a paginated list of products
    /// </summary>
    /// <remarks>
    /// Returns a paginated list of products based on the provided paging parameters.
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(PagedList<ProductListItemResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ListProducts(
        [FromQuery] ListProductsRequest request,
        CancellationToken cancellationToken)
    {
        var query = new ListProductsQuery(request.PageNumber, request.PageSize);

        var result = await mediator.Send(query, cancellationToken);

        return ToActionResult(result);
    }

    /// <summary>
    /// Retrieve product by identifier
    /// </summary>
    /// <remarks>
    /// Returns the product details for the specified product identifier.
    /// </remarks>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductDetailsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(id);

        var result = await mediator.Send(query, cancellationToken);

        return ToActionResult(result);
    }

    /// <summary>
    /// Create a new product
    /// </summary>
    /// <remarks>
    /// Creates a new product with the provided name, description, amount, and currency.
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(ProductDetailsResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct(
        [FromBody] CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateProductCommand(
            request.Name,
            request.Description,
            request.Amount,
            request.Currency);

        var result = await mediator.Send(command, cancellationToken);

        return ToActionResult(result);
    }

    /// <summary>
    /// Delete product by identifier
    /// </summary>
    /// <remarks>
    /// Deletes the product associated with the specified identifier.
    /// </remarks>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteProduct(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteProductCommand(id);

        var result = await mediator.Send(command, cancellationToken);

        return ToActionResult(result);
    }
}
