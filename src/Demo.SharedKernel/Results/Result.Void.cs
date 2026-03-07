namespace Demo.SharedKernel.Results;

public class Result : Result<Result>
{
    protected Result()
    {
    }

    protected Result(ResultStatus status) : base(status)
    {
    }

    public static Result Success()
    {
        return new();
    }

    public static Result<T> Created<T>(T value, string location)
    {
        return Result<T>.Created(value, location);
    }

    public new static Result Error(params Error[] errors)
    {
        return new(ResultStatus.Error)
        {
            Errors = errors
        };
    }

    public new static Result Invalid(params Error[] errors)
    {
        return new(ResultStatus.Invalid)
        {
            Errors = errors
        };
    }

    public new static Result NotFound(params Error[] errors)
    {
        return new(ResultStatus.NotFound)
        {
            Errors = errors
        };
    }

    public new static Result Forbidden(params Error[] errors)
    {
        return new(ResultStatus.Forbidden)
        {
            Errors = errors
        };
    }

    public new static Result Unauthorized(params Error[] errors)
    {
        return new(ResultStatus.Unauthorized)
        {
            Errors = errors
        };
    }

    public new static Result Conflict(params Error[] errors)
    {
        return new(ResultStatus.Conflict)
        {
            Errors = errors
        };
    }

    public new static Result CriticalError(params Error[] errors)
    {
        return new(ResultStatus.CriticalError)
        {
            Errors = errors
        };
    }

    public new static Result Unavailable(params Error[] errors)
    {
        return new(ResultStatus.Unavailable)
        {
            Errors = errors
        };
    }

    public new static Result NoContent()
    {
        return new(ResultStatus.NoContent);
    }
}
