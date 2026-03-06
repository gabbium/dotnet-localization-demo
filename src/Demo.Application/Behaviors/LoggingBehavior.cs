namespace Demo.Application.Behaviors;

public class LoggingBehavior<TMessage, TResponse>(
    ILogger<LoggingBehavior<TMessage, TResponse>> logger)
    : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage
{
    public async ValueTask<TResponse> Handle(
        TMessage message,
        MessageHandlerDelegate<TMessage, TResponse> next,
        CancellationToken cancellationToken)
    {
        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation(
                "Handling {MessageName} with {@Message}",
                typeof(TMessage).Name,
                message);
        }

        var sw = Stopwatch.StartNew();

        var response = await next(message, cancellationToken);

        sw.Stop();

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation(
                "Handled {MessageName} with {@Response} in {ElapsedMilliseconds} ms",
                typeof(TMessage).Name,
                response,
                sw.ElapsedMilliseconds);
        }

        return response;
    }
}
