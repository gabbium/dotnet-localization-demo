using Demo.SharedKernel.Results;

namespace Demo.Application.Errors;

public static class ProblemDetailsErrors
{
    public static Error BadRequestTitle() =>
        new("Error.BadRequest.Title");

    public static Error UnauthorizedTitle() =>
        new("Error.Unauthorized.Title");

    public static Error ForbiddenTitle() =>
        new("Error.Forbidden.Title");

    public static Error NotFoundTitle() =>
        new("Error.NotFound.Title");

    public static Error ConflictTitle() =>
        new("Error.Conflict.Title");

    public static Error UnprocessableTitle() =>
        new("Error.Unprocessable.Title");

    public static Error ServerFailureTitle() =>
        new("Error.ServerFailure.Title");

    public static Error ServerFailureDetail() =>
        new("Error.ServerFailure.Detail");

    public static Error ServiceUnavailableTitle() =>
        new("Error.ServiceUnavailable.Title");

    public static Error ValidationTitle() =>
        new("Error.Validation.Title");

    public static Error ValidationDetail() =>
        new("Error.Validation.Detail");
}
