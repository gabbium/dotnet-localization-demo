namespace Demo.Api.Infrastructure.Controllers;

public partial class KebabCaseParameterTransformer : IOutboundParameterTransformer
{
    [GeneratedRegex("([a-z])([A-Z])")]
    private static partial Regex KebabCaseRegex();

    public string? TransformOutbound(object? value)
    {
        if (value == null)
        {
            return null;
        }

        var input = value.ToString();

        return input == null
            ? null
            : KebabCaseRegex().Replace(input, "$1-$2").ToLower();
    }
}

