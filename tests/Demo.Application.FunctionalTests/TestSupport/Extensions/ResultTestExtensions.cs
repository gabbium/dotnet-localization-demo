using Demo.SharedKernel.Results;

namespace Demo.Application.FunctionalTests.TestSupport.Extensions;

public static class ResultTestExtensions
{
    public static Error ShouldHaveSingleError(this Result result)
        => result.Errors.ShouldHaveSingleItem();

    public static Error ShouldHaveSingleError<T>(this Result<T> result)
        => result.Errors.ShouldHaveSingleItem();
}
