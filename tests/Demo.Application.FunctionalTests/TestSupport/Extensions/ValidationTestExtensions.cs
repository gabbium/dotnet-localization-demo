using Demo.SharedKernel.Results;

namespace Demo.Application.FunctionalTests.TestSupport.Extensions;

public static class ValidationTestExtensions
{
    public static Error ShouldHaveSingleError(this ValidationException exception)
    {
        var failure = exception.Errors.ShouldHaveSingleItem();
        return (Error)failure.CustomState.ShouldNotBeNull();
    }
}
