namespace Demo.Api.Infrastructure;

public class DefaultExceptionHandler : IExceptionHandler
{
    private readonly ILogger<DefaultExceptionHandler> _logger;
    private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;

    public DefaultExceptionHandler(ILogger<DefaultExceptionHandler> logger)
    {
        _logger = logger;
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
            title: "Server failure",
            detail: "An unexpected error occurred",
            statusCode: StatusCodes.Status500InternalServerError,
            type: "https://datatracker.ietf.org/doc/html/rfc9110#section-15.6.1");

        await problem.ExecuteAsync(httpContext);

        return true;
    }

    private static async Task HandleValidationException(
        HttpContext httpContext,
        Exception ex)
    {
        var exception = (FluentValidation.ValidationException)ex;

        var errors = exception.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group.Select(e => e.ErrorMessage).ToArray());

        var problem = Results.ValidationProblem(
            title: "Bad Request",
            detail: "One or more validation errors occurred.",
            errors: errors,
            statusCode: StatusCodes.Status400BadRequest,
            type: "https://tools.ietf.org/html/rfc9110#section-15.5.1");

        await problem.ExecuteAsync(httpContext);
    }
}
