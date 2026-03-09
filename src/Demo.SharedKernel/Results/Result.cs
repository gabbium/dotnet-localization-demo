namespace Demo.SharedKernel.Results;

public class Result<T> : IResult
{
    public T? Value { get; init; }
    public ResultStatus Status { get; protected set; } = ResultStatus.Ok;
    public string? Location { get; protected set; }
    public IReadOnlyList<Error> Errors { get; protected set; } = [];
    public bool IsSuccess => Status is ResultStatus.Ok or ResultStatus.Created or ResultStatus.NoContent;
    public Type ValueType => typeof(T);
    public object? GetValue() => Value;

    protected Result()
    {
    }

    protected Result(ResultStatus status)
    {
        Status = status;
    }

    protected Result(T value)
    {
        Value = value;
        Status = ResultStatus.Ok;
    }

    public static Result<T> Success(T value) => new(value);

    public static Result<T> Created(T value, string location)
    {
        return new(ResultStatus.Created)
        {
            Value = value,
            Location = location
        };
    }

    public static Result<T> Error(params Error[] errors)
    {
        return new(ResultStatus.Error)
        {
            Errors = errors
        };
    }

    public static Result<T> Invalid(params Error[] errors)
    {
        return new(ResultStatus.Invalid)
        {
            Errors = errors
        };
    }

    public static Result<T> NotFound(params Error[] errors)
    {
        return new(ResultStatus.NotFound)
        {
            Errors = errors
        };
    }

    public static Result<T> Forbidden(params Error[] errors)
    {
        return new(ResultStatus.Forbidden)
        {
            Errors = errors
        };
    }

    public static Result<T> Unauthorized(params Error[] errors)
    {
        return new(ResultStatus.Unauthorized)
        {
            Errors = errors
        };
    }

    public static Result<T> Conflict(params Error[] errors)
    {
        return new(ResultStatus.Conflict)
        {
            Errors = errors
        };
    }

    public static Result<T> CriticalError(params Error[] errors)
    {
        return new(ResultStatus.CriticalError)
        {
            Errors = errors
        };
    }

    public static Result<T> Unavailable(params Error[] errors)
    {
        return new(ResultStatus.Unavailable)
        {
            Errors = errors
        };
    }

    public static Result<T> NoContent() => new(ResultStatus.NoContent);

    public static implicit operator T(Result<T> result)
    {
        return result.Value!;
    }

    public static implicit operator Result<T>(T value)
    {
        return new Result<T>(value);
    }

    public static implicit operator Result<T>(Result result)
    {
        return new(ResultStatus.Ok)
        {
            Status = result.Status,
            Errors = result.Errors,
            Location = result.Location
        };
    }
}
