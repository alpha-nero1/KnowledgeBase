using Scheduler.Core.Enums;
using Scheduler.Domain.Entities;

namespace Scheduler.Application.Scheduling.Interfaces;

/// <summary>
/// A service to manage scheduling of future jobs.
/// </summary>
public interface ISchedulerService
{
    Task<FutureJob> ScheduleJobAsync(int orderId, FutureJobType jobType, DateTime executeAt);
    Task CancelJobAsync(int futureJobId);
    Task<FutureJob> RescheduleJobAsync(int futureJobId, DateTime newExecuteAt);
}
