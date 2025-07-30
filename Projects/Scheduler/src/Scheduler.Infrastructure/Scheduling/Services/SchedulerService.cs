using Hangfire;
using Scheduler.Application.Scheduling.Interfaces;
using Scheduler.Contracts.Scheduling;
using Scheduler.Domain.Entities;
using DomainTaskStatus = Scheduler.Domain.Entities.TaskStatus;

namespace Scheduler.Infrastructure.Scheduling.Services;

public class SchedulerService : ISchedulerService
{
    private readonly IScheduledTaskRepository _scheduledTaskRepository;
    private readonly IBackgroundJobClient _backgroundJobClient;

    public SchedulerService(
        IScheduledTaskRepository scheduledTaskRepository,
        IBackgroundJobClient backgroundJobClient)
    {
        _scheduledTaskRepository = scheduledTaskRepository;
        _backgroundJobClient = backgroundJobClient;
    }

    public async Task<string> ScheduleJobAsync(Job job)
    {
        // Create domain entity
        var scheduledTask = new ScheduledTask
        {
            OrderId = job.OrderId,
            Type = job.Type,
            ExecuteAt = job.ExecuteAt,
            CreatedAt = DateTime.UtcNow,
            Status = DomainTaskStatus.Pending
        };

        // Save to database first
        var savedTask = await _scheduledTaskRepository.AddAsync(scheduledTask);

        // Schedule with Hangfire
        var hangfireJobId = _backgroundJobClient.Schedule<IJobExecutor>(
            executor => executor.ExecuteJobAsync(job),
            job.ExecuteAt);

        // Update with Hangfire job ID
        savedTask.HangfireJobId = hangfireJobId;
        savedTask.Status = DomainTaskStatus.Scheduled;
        await _scheduledTaskRepository.UpdateAsync(savedTask);

        return hangfireJobId ?? string.Empty;
    }

    public async Task CancelJobAsync(string jobId)
    {
        // Find task by Hangfire job ID
        var task = await _scheduledTaskRepository.GetByHangfireJobIdAsync(jobId);
        if (task != null)
        {
            // Cancel in Hangfire
            _backgroundJobClient.Delete(jobId);
            
            // Update status in database
            task.Status = DomainTaskStatus.Failed;
            task.ErrorMessage = "Job cancelled";
            await _scheduledTaskRepository.UpdateAsync(task);
        }
    }

    public async Task<bool> DeleteJobAsync(string jobId)
    {
        // Find task by Hangfire job ID
        var task = await _scheduledTaskRepository.GetByHangfireJobIdAsync(jobId);
        if (task != null)
        {
            // Delete from Hangfire
            _backgroundJobClient.Delete(jobId);
            
            // Delete from database
            await _scheduledTaskRepository.DeleteAsync(task.Id);
            return true;
        }
        return false;
    }

    public Task<bool> RescheduleJobAsync(string jobId, DateTime newExecuteTime)
    {
        throw new NotImplementedException();
    }
}
