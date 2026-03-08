namespace Demo.SharedKernel.Results;

public sealed record Error(string Code, params object[] Parameters);
