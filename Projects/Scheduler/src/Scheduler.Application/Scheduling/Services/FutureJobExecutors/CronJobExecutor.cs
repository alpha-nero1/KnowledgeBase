using Microsoft.Extensions.Logging;
using Scheduler.Application.Scheduling.Interfaces;
using Scheduler.Contracts.Scheduling;
using Scheduler.Core.Enums;

namespace Scheduler.Application.Scheduling.Services.FutureJobExecutors;

/// <summary>
/// CRON job executor for recurring tasks that run on a schedule.
/// </summary>
public class MidnightCleanupJobExecutor(ILogger<MidnightCleanupJobExecutor> _logger) : IFutureJobExecutor
{
    public FutureJobType Type => FutureJobType.MidnightCleanupJob;

    public Task ExecuteAsync(FutureJobDto futureJob)
    {
        _logger.LogInformation("Executing CronJobExecutor for FutureJobId: {FutureJobId}, OrderId: {OrderId}, Type: {Type}, ExecuteAt: {ExecuteAt}",
            futureJob.FutureJobId,
            futureJob.OrderId,
            futureJob.Type,
            futureJob.ExecuteAt
        );

        // Add your custom CRON job logic here
        // This is where you would implement the actual work that needs to be done daily at midnight
        _logger.LogInformation("Daily CRON job executed successfully at {ExecutedAt}", DateTime.UtcNow);
        
        return Task.CompletedTask;
    }
}
