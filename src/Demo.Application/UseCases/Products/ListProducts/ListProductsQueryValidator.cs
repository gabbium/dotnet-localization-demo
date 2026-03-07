using Demo.Application.Errors;

namespace Demo.Application.UseCases.Products.ListProducts;

public class ListProductsQueryValidator : AbstractValidator<ListProductsQuery>
{
    public ListProductsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithState(_ => PaginationErrors.PageNumberMustBeGreaterThanOrEqualTo(1));

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100)
            .WithState(_ => PaginationErrors.PageSizeMustBeBetween(1, 100));
    }
}
