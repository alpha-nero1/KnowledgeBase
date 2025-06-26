using common;
using MassTransit;

namespace worker;

public class OrderUpdateEventConsumer : IConsumer<OrderUpdateEvent>
{
    private readonly ILogger<OrderUpdateEventConsumer> _logger;

    public OrderUpdateEventConsumer(ILogger<OrderUpdateEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<OrderUpdateEvent> ctx)
    {
        var message = ctx.Message;

        _logger.LogInformation("Worker received message {message}", message);
        bool shouldFail = new Random().Next(0, 10) > 5;
        // Will throw message into the DLQ.
        if (shouldFail)
        {
            throw new Exception("Failed to process event");
        }

        return Task.CompletedTask;
    }
}
