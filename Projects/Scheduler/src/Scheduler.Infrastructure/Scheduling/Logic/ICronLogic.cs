namespace Scheduler.Infrastructure.Scheduling.Logic;

public interface ICronLogic
{
    /// <summary>
    /// Given a CRON expression, returns the next execution time.
    /// </summary>
    DateTime? GetNextExectionDateTime(string cronExpression, DateTimeOffset? from = null);
}
