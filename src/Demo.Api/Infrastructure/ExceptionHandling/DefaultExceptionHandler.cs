using Demo.Api.Infrastructure.ProblemDetails;
using Demo.Api.Resources;
using Demo.SharedKernel.Results;

namespace Demo.Api.Infrastructure.ExceptionHandling;

public class DefaultExceptionHandler : IExceptionHandler
{
    private readonly ILogger<DefaultExceptionHandler> _logger;
    private readonly IStringLocalizer<SharedResource> _localizer;

    private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;

    public DefaultExceptionHandler(ILogger<DefaultExceptionHandler> logger, IStringLocalizer<SharedResource> localizer)
    {
        _logger = logger;
        _localizer = localizer;

        _exceptionHandlers = new()
        {
            { typeof(FluentValidation.ValidationException), HandleValidationException },
        };
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var exceptionType = exception.GetType();

        if (_exceptionHandlers.TryGetValue(
            exceptionType,
            out Func<HttpContext, Exception, Task>? exceptionHandler))
        {
            await exceptionHandler.Invoke(httpContext, exception);
            return true;
        }

        _logger.LogError(exception, "An unhandled exception has occurred while executing the request");

        var problem = Results.Problem(
            title: _localizer[ProblemDetailsErrors.ServerFailureTitle().Code],
            detail: _localizer[ProblemDetailsErrors.ServerFailureDetail().Code],
            statusCode: StatusCodes.Status500InternalServerError,
            type: "https://datatracker.ietf.org/doc/html/rfc9110#section-15.6.1");

        await problem.ExecuteAsync(httpContext);

        return true;
    }

    private async Task HandleValidationException(
        HttpContext httpContext,
        Exception ex)
    {
        var exception = (FluentValidation.ValidationException)ex;

        var errors = exception.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e =>
                {
                    if (e.CustomState is Error error)
                    {
                        return _localizer[error.Code, error.Arguments].Value;
                    }

                    return _localizer[e.ErrorCode].Value;
                }).ToArray());

        var problem = Results.ValidationProblem(
            title: _localizer[ProblemDetailsErrors.ValidationTitle().Code],
            detail: _localizer[ProblemDetailsErrors.ValidationDetail().Code],
            errors: errors,
            statusCode: StatusCodes.Status400BadRequest,
            type: "https://tools.ietf.org/html/rfc9110#section-15.5.1");

        await problem.ExecuteAsync(httpContext);
    }
}
