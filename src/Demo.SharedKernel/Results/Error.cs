namespace Demo.SharedKernel.Results;

public class Error(string code, params object[] arguments)
{
    public string Code { get; } = code;
    public object[] Arguments { get; } = arguments;
}
