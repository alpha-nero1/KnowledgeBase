using System.Text.Json;
using StackExchange.Redis;

namespace worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    public Worker(ILogger<Worker> logger, IConnectionMultiplexer connectionMultiplexer)
    {
        _logger = logger;
        _connectionMultiplexer = connectionMultiplexer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            await Task.Delay(10000, stoppingToken);
            // Notify!
            var publisher = _connectionMultiplexer.GetSubscriber();
            var message = new
            {
                Recipient = "ale",
                Sender = "system",
                Message = "Your report is ready!",
                Timestamp = DateTimeOffset.Now
            };
            var json = JsonSerializer.Serialize(message);
            await publisher.PublishAsync("notify.users", json);
            _logger.LogInformation("Worker published message {message} at {time}", message, DateTimeOffset.Now);
        }
    }
}
