using Scheduler.Domain.Entities;

namespace Scheduler.Application.Scheduling.Interfaces;

public interface IScheduledTaskRepository
{
    Task<ScheduledTask> AddAsync(ScheduledTask scheduledTask);
    Task<ScheduledTask?> GetByIdAsync(int id);
    Task<ScheduledTask?> GetByHangfireJobIdAsync(string hangfireJobId);
    Task<IEnumerable<ScheduledTask>> GetByOrderIdAsync(int orderId);
    Task<IEnumerable<ScheduledTask>> GetPendingTasksAsync();
    Task<ScheduledTask> UpdateAsync(ScheduledTask scheduledTask);
    Task DeleteAsync(int id);
}
