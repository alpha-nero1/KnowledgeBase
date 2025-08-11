namespace Scheduler.Application.Scheduling.Interfaces;

/// <summary>
/// Service for managing recurring CRON jobs.
/// </summary>
public interface ICronSchedulerService
{
    /// <summary>
    /// Schedule a recurring job using CRON expression.
    /// </summary>
    /// <param name="jobId">Unique identifier for the recurring job</param>
    /// <param name="cronExpression">CRON expression (e.g., "0 0 * * *" for daily at midnight)</param>
    /// <param name="orderId">Order ID associated with the job</param>
    /// <returns>The scheduled job ID</returns>
    string ScheduleRecurringJob(string jobId, string cronExpression, int orderId);

    /// <summary>
    /// Schedule a daily job to run at midnight UTC.
    /// </summary>
    /// <param name="jobId">Unique identifier for the recurring job</param>
    /// <param name="orderId">Order ID associated with the job</param>
    /// <returns>The scheduled job ID</returns>
    string ScheduleDailyMidnightJob(string jobId, int orderId);

    /// <summary>
    /// Remove a recurring job.
    /// </summary>
    /// <param name="jobId">The job identifier to remove</param>
    void RemoveRecurringJob(string jobId);

    /// <summary>
    /// Check if a recurring job exists.
    /// </summary>
    /// <param name="jobId">The job identifier to check</param>
    /// <returns>True if the job exists, false otherwise</returns>
    bool JobExists(string jobId);
}
