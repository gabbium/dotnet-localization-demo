namespace Demo.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected IActionResult ToActionResult(Ardalis.Result.IResult result)
    {
        return result.Status switch
        {
            ResultStatus.Ok => Ok(result),
            ResultStatus.Created => Created(result),
            ResultStatus.NoContent => NoContent(result),
            ResultStatus.Invalid => Invalid(result),
            ResultStatus.Unauthorized => Unauthorized(result),
            ResultStatus.Forbidden => Forbidden(result),
            ResultStatus.NotFound => NotFound(result),
            ResultStatus.Conflict => Conflict(result),
            ResultStatus.Error => Unprocessable(result),
            ResultStatus.CriticalError => CriticalError(result),
            ResultStatus.Unavailable => Unavailable(result),
            _ => throw new NotSupportedException($"Status {result.Status} not supported")
        };
    }

    private IActionResult Ok(Ardalis.Result.IResult result)
    {
        return result is Result
            ? base.Ok()
            : base.Ok(result.GetValue());
    }

    private CreatedResult Created(Ardalis.Result.IResult result)
    {
        return base.Created(string.Empty, result.GetValue());
    }

    private NoContentResult NoContent(Ardalis.Result.IResult result)
    {
        return base.NoContent();
    }

    private ActionResult Invalid(Ardalis.Result.IResult result)
    {
        if (result.ValidationErrors == null || !result.ValidationErrors.Any())
        {
            return BadRequest(BuildErrorDetails(result));
        }

        var dict = new ModelStateDictionary();

        foreach (var e in result.ValidationErrors)
        {
            var key = e.Identifier ?? string.Empty;
            dict.AddModelError(key, e.ErrorMessage);
        }

        return ValidationProblem(dict);
    }

    private ObjectResult Unauthorized(Ardalis.Result.IResult result)
    {
        return Problem(
            title: "Unauthorized",
            detail: BuildErrorDetails(result),
            statusCode: StatusCodes.Status401Unauthorized);
    }

    private ObjectResult Forbidden(Ardalis.Result.IResult result)
    {
        return Problem(
            title: "Forbidden",
            detail: BuildErrorDetails(result),
            statusCode: StatusCodes.Status403Forbidden);
    }

    private ObjectResult NotFound(Ardalis.Result.IResult result)
    {
        return Problem(
            title: "Not Found",
            detail: BuildErrorDetails(result),
            statusCode: StatusCodes.Status404NotFound);
    }

    private ObjectResult Conflict(Ardalis.Result.IResult result)
    {
        return Problem(
            title: "Conflict",
            detail: BuildErrorDetails(result),
            statusCode: StatusCodes.Status409Conflict);
    }

    private ObjectResult Unprocessable(Ardalis.Result.IResult result)
    {
        return Problem(
            title: "Unprocessable Entity",
            detail: BuildErrorDetails(result),
            statusCode: StatusCodes.Status422UnprocessableEntity);
    }

    private ObjectResult CriticalError(Ardalis.Result.IResult result)
    {
        return Problem(
            title: "Server Failure",
            detail: BuildErrorDetails(result),
            statusCode: StatusCodes.Status500InternalServerError);
    }

    private ObjectResult Unavailable(Ardalis.Result.IResult result)
    {
        return Problem(
            title: "Service Unavailable",
            detail: BuildErrorDetails(result),
            statusCode: StatusCodes.Status503ServiceUnavailable);
    }

    private static string BuildErrorDetails(Ardalis.Result.IResult result)
    {
        if (result.Errors == null || !result.Errors.Any())
        {
            return "An unexpected error occurred.";
        }

        if (result.Errors.Count() == 1)
        {
            return result.Errors.First();
        }

        var sb = new StringBuilder("Errors:");

        foreach (var error in result.Errors)
        {
            sb.AppendLine().Append("* ").Append(error);
        }

        return sb.ToString();
    }
}
