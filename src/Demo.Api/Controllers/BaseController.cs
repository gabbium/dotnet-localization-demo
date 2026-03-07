using Demo.Api.Resources;
using Demo.Application.Errors;
using Demo.SharedKernel.Results;

namespace Demo.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseController(IStringLocalizer<SharedResource> localizer) : ControllerBase
{
    protected IActionResult ToActionResult(SharedKernel.Results.IResult result)
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

    private IActionResult Ok(SharedKernel.Results.IResult result)
    {
        return result is Result
            ? base.Ok()
            : base.Ok(result.GetValue());
    }

    private CreatedResult Created(SharedKernel.Results.IResult result)
    {
        return base.Created(string.Empty, result.GetValue());
    }

    private NoContentResult NoContent(SharedKernel.Results.IResult result)
    {
        return base.NoContent();
    }

    private ObjectResult Invalid(SharedKernel.Results.IResult result)
    {
        return Problem(
            title: localizer[ProblemDetailsErrors.BadRequestTitle().Code],
            detail: BuildErrorDetails(result),
            statusCode: StatusCodes.Status400BadRequest);
    }

    private ObjectResult Unauthorized(SharedKernel.Results.IResult result)
    {
        return Problem(
            title: localizer[ProblemDetailsErrors.UnauthorizedTitle().Code],
            detail: BuildErrorDetails(result),
            statusCode: StatusCodes.Status401Unauthorized);
    }

    private ObjectResult Forbidden(SharedKernel.Results.IResult result)
    {
        return Problem(
            title: localizer[ProblemDetailsErrors.ForbiddenTitle().Code],
            detail: BuildErrorDetails(result),
            statusCode: StatusCodes.Status403Forbidden);
    }

    private ObjectResult NotFound(SharedKernel.Results.IResult result)
    {
        return Problem(
            title: localizer[ProblemDetailsErrors.NotFoundTitle().Code],
            detail: BuildErrorDetails(result),
            statusCode: StatusCodes.Status404NotFound);
    }

    private ObjectResult Conflict(SharedKernel.Results.IResult result)
    {
        return Problem(
            title: localizer[ProblemDetailsErrors.ConflictTitle().Code],
            detail: BuildErrorDetails(result),
            statusCode: StatusCodes.Status409Conflict);
    }

    private ObjectResult Unprocessable(SharedKernel.Results.IResult result)
    {
        return Problem(
            title: localizer[ProblemDetailsErrors.UnprocessableTitle().Code],
            detail: BuildErrorDetails(result),
            statusCode: StatusCodes.Status422UnprocessableEntity);
    }

    private ObjectResult CriticalError(SharedKernel.Results.IResult result)
    {
        return Problem(
            title: localizer[ProblemDetailsErrors.ServerFailureTitle().Code],
            detail: BuildErrorDetails(result),
            statusCode: StatusCodes.Status500InternalServerError);
    }

    private ObjectResult Unavailable(SharedKernel.Results.IResult result)
    {
        return Problem(
            title: localizer[ProblemDetailsErrors.ServiceUnavailableTitle().Code],
            detail: BuildErrorDetails(result),
            statusCode: StatusCodes.Status503ServiceUnavailable);
    }

    private string BuildErrorDetails(SharedKernel.Results.IResult result)
    {
        if (result.Errors == null || !result.Errors.Any())
        {
            return localizer[ProblemDetailsErrors.ServerFailureDetail().Code];
        }

        if (result.Errors.Count == 1)
        {
            var error = result.Errors[0];
            return localizer[error.Code, error.Arguments];
        }

        var sb = new StringBuilder();

        foreach (var error in result.Errors)
        {
            sb.AppendLine().Append("* ").Append(localizer[error.Code, error.Arguments]);
        }

        return sb.ToString();
    }
}
