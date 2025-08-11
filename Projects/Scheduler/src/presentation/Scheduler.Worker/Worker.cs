namespace Scheduler.Worker;

/// <summary>
/// This worker is only needed to keep the background service allive.
/// Hangifre will be responsible for finding the right namespace and invoking the
/// scheduled job directly.
/// </summary>
public class Worker(ILogger<Worker> _logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
        _logger.LogInformation("Worker stopping at: {time}", DateTimeOffset.Now);
    }
}
