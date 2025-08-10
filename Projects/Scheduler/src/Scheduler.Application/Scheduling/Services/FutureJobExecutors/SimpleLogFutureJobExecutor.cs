using Microsoft.Extensions.Logging;
using Scheduler.Application.Scheduling.Interfaces;
using Scheduler.Contracts.Scheduling;
using Scheduler.Core.Enums;

namespace Scheduler.Application.Scheduling.Services.FutureJobExecutors;

/// <summary>
/// Simple log executor for demonstration purposes.
/// </summary>
public class SimpleLogFutureJobExecutor(ILogger<SimpleLogFutureJobExecutor> _logger) : IFutureJobExecutor
{
    public FutureJobType Type => FutureJobType.SimpleLog;
    public Task ExecuteAsync(FutureJobDto futureJob)
    {
        _logger.LogInformation("Executing SimpleLogFutureJobExecutor for FutureJobId: {FutureJobId}, OrderId: {OrderId}, Type: {Type}, ExecuteAt: {ExecuteAt}",
            futureJob.FutureJobId,
            futureJob.OrderId,
            futureJob.Type,
            futureJob.ExecuteAt
        );
        return Task.CompletedTask;
    }
}