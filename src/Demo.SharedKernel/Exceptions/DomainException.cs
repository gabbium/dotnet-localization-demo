using Demo.SharedKernel.Results;

namespace Demo.SharedKernel.Exceptions;

public class DomainException(Error error) : Exception(error.Code)
{
    public Error Error { get; } = error;
}
