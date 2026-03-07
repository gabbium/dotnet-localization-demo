using Demo.SharedKernel.Results;

namespace Demo.Application.Errors;

public static class PaginationErrors
{
    public static Error PageNumberMustBeGreaterThanOrEqualTo(int min) =>
        new("Pagination.PageNumber.MustBeGreaterThanOrEqualTo", min);

    public static Error PageSizeMustBeBetween(int min, int max) =>
        new("Pagination.PageSize.MustBeBetween", min, max);
}
