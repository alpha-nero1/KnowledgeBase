using Cronos;

namespace Scheduler.Infrastructure.Scheduling.Logic;

public class CronLogic : ICronLogic
{
    public DateTime? GetNextExectionDateTime(string cronExpression, DateTimeOffset? from = null)
    {
        var cronSchedule = CronExpression.Parse(cronExpression);
        var nextExecution = cronSchedule.GetNextOccurrence(from ?? DateTimeOffset.UtcNow, TimeZoneInfo.Utc);
        return nextExecution?.UtcDateTime;
    }
}
