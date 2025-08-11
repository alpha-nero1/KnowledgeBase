using Scheduler.Core.Enums;
using Scheduler.Domain.Entities;

namespace Scheduler.Application.Scheduling.Interfaces;

/// <summary>
/// A service to manage scheduling of future jobs.
/// </summary>
public interface ISchedulerService
{
    Task<FutureJob> ScheduleJobAsync(int orderId, FutureJobType jobType, DateTime executeAt);
    Task<FutureJob> ScheduleRecurringJob(int orderId, FutureJobType type, string cronExpression = "0 0 * * *");
    Task CancelJobAsync(int futureJobId);
}
