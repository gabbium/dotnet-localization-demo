using Demo.Api.Models;
using Demo.Api.Resources;
using Demo.Application.Models;
using Demo.Application.UseCases.Products.Commands.CreateProduct;
using Demo.Application.UseCases.Products.Queries.GetProductById;
using Demo.Application.UseCases.Products.Queries.ListProducts;
using Demo.SharedKernel.Pagination;

namespace Demo.Api.Controllers;

public class ProductsController(IMediator mediator, IStringLocalizer<SharedResource> localizer) : BaseController(localizer)
{
    /// <summary>
    /// Retrieve paginated list of products
    /// </summary>
    /// <remarks>
    /// Returns a paginated list of products based on the provided paging parameters.
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(PagedList<ProductListItemResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ListProducts([FromQuery] ListProductsRequest request, CancellationToken cancellationToken)
    {
        var query = new ListProductsQuery(request.PageNumber, request.PageSize);

        var result = await mediator.Send(query, cancellationToken);

        return ToActionResult(result);
    }

    /// <summary>
    /// Retrieve product by identifier
    /// </summary>
    /// <remarks>
    /// Returns the product details for the specified unique identifier.
    /// </remarks>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductDetailsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(id);

        var result = await mediator.Send(query, cancellationToken);

        return ToActionResult(result);
    }

    /// <summary>
    /// Create a new product
    /// </summary>
    /// <remarks>
    /// Creates a new product in draft status with the provided name, description, and price.
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
}
