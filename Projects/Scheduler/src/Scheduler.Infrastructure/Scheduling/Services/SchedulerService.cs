using Hangfire;
using Scheduler.Application.Scheduling.Interfaces;
using Scheduler.Contracts.Scheduling;
using Scheduler.Core.Enums;
using Scheduler.Domain.Entities;

namespace Scheduler.Infrastructure.Scheduling.Services;

public class SchedulerService(
    IFutureJobRepository _futureJobRepository,
    IBackgroundJobClient _backgroundJobClient,
    IEnumerable<IFutureJobExecutor> _executors
) : ISchedulerService
{
    public async Task<FutureJob> ScheduleJobAsync(int orderId, FutureJobType type, DateTime executeAt)
    {
        // Create domain entity
        var scheduledTask = new FutureJob
        {
            OrderId = orderId,
            Type = type,
            ExecuteAt = executeAt,
            // Note: replace this use later with IDateTime
            CreatedAt = DateTime.UtcNow,
            Status = FutureJobStatus.Pending
        };

        // Save to database first
        var savedJob = await _futureJobRepository.AddAsync(scheduledTask);

        // Schedule with Hangfire
        var executor = _executors.FirstOrDefault(e => e.Type == type);
        if (executor == null)
        {
            throw new InvalidOperationException($"No executor found for job type: {type}, orderId: {orderId}");
        }

        var hangfireJobId = _backgroundJobClient.Schedule(
            () => executor.ExecuteAsync(new FutureJobDto
            {
                FutureJobId = savedJob.FutureJobId,
                OrderId = savedJob.OrderId,
                Type = savedJob.Type,
                ExecuteAt = savedJob.ExecuteAt
            }),
            savedJob.ExecuteAt
        );

        if (string.IsNullOrEmpty(hangfireJobId))
        {
            throw new InvalidOperationException($"Failed to retrieve Hangfire job ID for job type: {type}, orderId: {orderId}");
        }

        // Update with Hangfire job ID
        savedJob.HangfireJobId = hangfireJobId;
        savedJob.Status = FutureJobStatus.Scheduled;
        await _futureJobRepository.UpdateAsync(savedJob);

        return savedJob;
    }

    public async Task CancelJobAsync(int jobId)
    {
        // Find task by Hangfire job ID
        var futureJob = await _futureJobRepository.GetByIdAsync(jobId);
        if (futureJob == null) return;

        // Cancel in Hangfire
        _backgroundJobClient.Delete(futureJob.HangfireJobId);
        
        // Update status in database
        futureJob.Status = FutureJobStatus.Cancelled;
        await _futureJobRepository.UpdateAsync(futureJob);
    }

    public async Task<bool> DeleteJobAsync(int jobId)
    {
        // Find task by Hangfire job ID
        var futureJob = await _futureJobRepository.GetByIdAsync(jobId);
        if (futureJob == null) return false;

        // Delete from Hangfire
        _backgroundJobClient.Delete(futureJob.HangfireJobId);
        
        // Delete from database
        await _futureJobRepository.DeleteAsync(futureJob.FutureJobId);
        return true;
    }

    public async Task<FutureJob> RescheduleJobAsync(int futureJobId, DateTime newExecuteAt)
    {
        // Get the existing job
        var futureJob = await _futureJobRepository.GetByIdAsync(futureJobId);
        if (futureJob == null) 
        {
            throw new InvalidOperationException($"Could not find job {futureJobId} to reschedule.");
        }

        try
        {
            // Cancel the existing Hangfire job if it exists
            if (!string.IsNullOrEmpty(futureJob.HangfireJobId))
            {
                _backgroundJobClient.Delete(futureJob.HangfireJobId);
            }

            // Find the correct executor for rescheduling
            var executor = _executors.FirstOrDefault(e => e.Type == futureJob.Type);
            if (executor == null)
            {
                throw new InvalidOperationException($"No executor found for job type: {futureJob.Type}");
            }

            // Schedule the job with the new execution time
            var newHangfireJobId = _backgroundJobClient.Schedule(
                () => executor.ExecuteAsync(new FutureJobDto
                {
                    FutureJobId = futureJob.FutureJobId,
                    OrderId = futureJob.OrderId,
                    Type = futureJob.Type,
                    ExecuteAt = newExecuteAt
                }),
                newExecuteAt
            );

            if (string.IsNullOrEmpty(newHangfireJobId))
            {
                throw new InvalidOperationException($"Failed to reschedule job with ID: {futureJobId}");
            }

            // Update the job with new details
            futureJob.ExecuteAt = newExecuteAt;
            futureJob.HangfireJobId = newHangfireJobId;
            futureJob.Status = FutureJobStatus.Scheduled;
            futureJob.UpdatedAt = DateTime.UtcNow;
            futureJob.CreatedAt = DateTime.UtcNow;
            
            await _futureJobRepository.UpdateAsync(futureJob);
            
            return futureJob;
        }
        catch
        {
            // If rescheduling fails, mark the job as failed
            futureJob.Status = FutureJobStatus.Failed;
            await _futureJobRepository.UpdateAsync(futureJob);
            throw;
        }
    }
}
