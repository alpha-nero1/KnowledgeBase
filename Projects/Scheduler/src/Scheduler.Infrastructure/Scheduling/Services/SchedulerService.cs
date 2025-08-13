using Hangfire;
using Microsoft.Extensions.Logging;
using Scheduler.Application.Scheduling.Interfaces;
using Scheduler.Contracts.Scheduling;
using Scheduler.Core.Enums;
using Scheduler.Domain.Entities;
using Scheduler.Infrastructure.Scheduling.Logic;

namespace Scheduler.Infrastructure.Scheduling.Services;

public class SchedulerService(
    IFutureJobRepository _futureJobRepository,
    IRecurringJobManager _recurringJobManager,
    IBackgroundJobClient _backgroundJobClient,
    ICronLogic _cronLogic,
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
            () => ExecuteFutureJobAsync(type, ToJobDto(savedJob)),
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

    public async Task<FutureJob> ScheduleRecurringJob(int orderId, FutureJobType type, string cronExpression = "0 0 * * *")
    {
        var executor = _executors.FirstOrDefault(e => e.Type == type);
        if (executor == null)
        {
            throw new InvalidOperationException("No executor found for CRON job type");
        }

        var executeAt = _cronLogic.GetNextExectionDateTime(cronExpression);

        if (!executeAt.HasValue)
        {
            throw new InvalidOperationException("Next execution could not be calculated from CRON expression");
        }

        // Create a FutureJobDto for the recurring execution
        string hangfireJobId = $"recurring-{orderId}-{type}";
        var scheduledTask = new FutureJob
        {
            OrderId = orderId,
            Type = type,
            ExecuteAt = executeAt!.Value,
            Schedule = cronExpression,
            HangfireJobId = hangfireJobId,
            // Note: replace this use later with IDateTime
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Status = FutureJobStatus.Pending
        };

        var savedJob = await _futureJobRepository.AddAsync(scheduledTask);

        // Schedule the recurring job with Hangfire
        _recurringJobManager.AddOrUpdate(
            hangfireJobId,
            () => ExecuteCronJobAsync(type, ToJobDto(savedJob)),
            cronExpression,
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Utc
            }
        );

        return savedJob;
    }

    public async Task CancelJobAsync(int jobId)
    {
        // Find task by Hangfire job ID
        var futureJob = await _futureJobRepository.GetByIdAsync(jobId);
        if (futureJob == null) 
        {
            throw new InvalidOperationException($"Could not find job {jobId} to cancel.");
        }

        // Cancel in Hangfire
        if (string.IsNullOrEmpty(futureJob.Schedule)) 
        {
            _backgroundJobClient.Delete(futureJob.HangfireJobId);
        }
        else
        {
            _recurringJobManager.RemoveIfExists(futureJob.HangfireJobId);
        }
        
        // Update status in database
        futureJob.Status = FutureJobStatus.Cancelled;
        await _futureJobRepository.UpdateAsync(futureJob);
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

    /// <summary>
    /// Wrapper for executing a simple future job.
    ///  
    /// Note: must be public so that hangfire can find it.
    /// </summary>
    public async Task ExecuteFutureJobAsync(FutureJobType type, FutureJobDto futureJob)
    {
        // Watch out for this HUGE trap in hangfire. Do NOT pass in the executor directly to the function which hangfire
        // will execute. Said service will be out of the dependency injection scope that hangfire creates.
        // Anything DI must be discovered INSIDE your execution, not passed into it. Thank you goodbye.
        var executor = _executors.FirstOrDefault(e => e.Type == type);
        if (executor == null)
        {
            throw new InvalidOperationException($"No executor found for job type: {type}, orderId: {futureJob.OrderId}");
        }
        await executor.ExecuteAsync(futureJob);
        // If successful, update status and next execution time.
        var entity = await _futureJobRepository.GetByIdAsync(futureJob.FutureJobId);
        if (entity == null) 
        {
            throw new InvalidOperationException($"FutureJob with ID {futureJob.FutureJobId} not found.");
        }
        entity.Status = FutureJobStatus.Completed;
        entity.UpdatedAt = DateTime.UtcNow;
        await _futureJobRepository.UpdateAsync(entity);
    }

    /// <summary>
    /// Wrapper for executing a CRON scheduled job.
    ///  
    /// Note: must be public so that hangfire can find it.
    /// </summary>
    public async Task ExecuteCronJobAsync(FutureJobType type, FutureJobDto futureJob)
    {
        // Watch out for this HUGE trap in hangfire. Do NOT pass in the executor directly to the function which hangfire
        // will execute. Said service will be out of the dependency injection scope that hangfire creates.
        // Anything DI must be discovered INSIDE your execution, not passed into it. Thank you goodbye.
        var executor = _executors.FirstOrDefault(e => e.Type == type);
        if (executor == null)
        {
            throw new InvalidOperationException($"No executor found for job type: {type}, orderId: {futureJob.OrderId}");
        }
        await executor.ExecuteAsync(futureJob);
        // If successful, update status and next execution time.
        var entity = await _futureJobRepository.GetByIdAsync(futureJob.FutureJobId);
        if (entity is null) 
        {
            throw new InvalidOperationException($"FutureJob with ID {futureJob.FutureJobId} not found.");
        }
        entity.Status = FutureJobStatus.Completed;
        entity.UpdatedAt = DateTime.UtcNow;
        var executeAt = _cronLogic.GetNextExectionDateTime(entity.Schedule, DateTimeOffset.UtcNow);
        if (!executeAt.HasValue)
        {
            throw new InvalidOperationException("Next execution could not be calculated from CRON expression");
        }
        entity.ExecuteAt = executeAt.Value;
        await _futureJobRepository.UpdateAsync(entity);
    }

    private static FutureJobDto ToJobDto(FutureJob jb) => new()
        {
            FutureJobId = jb.FutureJobId,
            OrderId = jb.OrderId,
            Type = jb.Type,
            ExecuteAt = jb.ExecuteAt
        };
}
