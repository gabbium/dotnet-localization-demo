namespace Demo.SharedKernel.Results;

public interface IResult
{
    ResultStatus Status { get; }
    IReadOnlyList<Error> Errors { get; }
    Type ValueType { get; }
    object? GetValue();
    string? Location { get; }
    bool IsSuccess { get; }
}
