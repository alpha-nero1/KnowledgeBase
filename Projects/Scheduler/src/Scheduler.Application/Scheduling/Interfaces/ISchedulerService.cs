using Scheduler.Core.Enums;
using Scheduler.Domain.Entities;

namespace Scheduler.Application.Scheduling.Interfaces;

/// <summary>
/// A service to manage scheduling of future jobs.
/// </summary>
public interface ISchedulerService
{
    /// <summary>
    /// Schedule a simple job to execute in the future.
    /// </summary>
    Task<FutureJob> ScheduleJobAsync(int orderId, FutureJobType jobType, DateTime executeAt);

    /// <summary>
    /// Schedule a recurring job with a CRON expression. 
    /// </summary>
    Task<FutureJob> ScheduleRecurringJob(int orderId, FutureJobType type, string cronExpression = "0 0 * * *");
    
    /// <summary>
    /// Cancel a job, no matter what it is.
    /// </summary>
    Task CancelJobAsync(int futureJobId);
}
