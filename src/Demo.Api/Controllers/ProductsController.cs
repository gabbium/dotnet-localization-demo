using Demo.Api.Models;
using Demo.Api.Resources;
using Demo.Application.Models;
using Demo.Application.UseCases.Products.CreateProduct;
using Demo.Application.UseCases.Products.ListProducts;
using Demo.SharedKernel.Results;

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
    /// Create a new product
    /// </summary>
    /// <remarks>
    /// Creates a new product in draft status with the provided name, description, and price.
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(ProductDetailsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct(
        [FromBody] CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var teste = CultureInfo.CurrentCulture;

        var command = new CreateProductCommand(
            request.Name,
            request.Description,
            request.Amount,
            request.Currency);

        var result = await mediator.Send(command, cancellationToken);

        return ToActionResult(result);
    }
}
