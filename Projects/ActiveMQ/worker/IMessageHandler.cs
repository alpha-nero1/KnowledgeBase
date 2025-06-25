using common;
using Microsoft.Extensions.Logging;

namespace worker;

public interface IMessageHandler
{
    Task HandleMessage(DataMessage message);
}

public sealed class MessageHandler : IMessageHandler
{
    private readonly ILogger<IMessageHandler> _logger;

    public MessageHandler(ILogger<IMessageHandler> logger)
    {
        _logger = logger;
    }

    public async Task HandleMessage(DataMessage message)
    {
        for (int i = 0; i < 10; i++)
        {
            var percentComplete = (i + 1) / 10M * 100;
            _logger.LogInformation("Doing some work for {orderId}: {percentComplete}% complete", message.OrderId, percentComplete);
            await Task.Delay(1000);
        }
        _logger.LogInformation("Processing for {orderId} complete!", message.OrderId);
    }
}