using Scheduler.Contracts.Scheduling;

namespace Scheduler.Application.Scheduling.Interfaces;

public interface ISchedulerService
{
    Task<string> ScheduleJobAsync(Job job);
    Task CancelJobAsync(string jobId);
    Task<bool> RescheduleJobAsync(string jobId, DateTime newExecuteTime);
}
