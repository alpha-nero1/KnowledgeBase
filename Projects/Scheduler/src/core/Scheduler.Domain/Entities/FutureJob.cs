using Scheduler.Core.Enums;

namespace Scheduler.Domain.Entities;

public class FutureJob : TrackableEntity
{
    public int FutureJobId { get; set; }
    public int OrderId { get; set; }
    public FutureJobType Type { get; set; }

    /// <summary>
    /// Will contain the date and time when the job should be executed.
    /// </summary>
    public DateTime ExecuteAt { get; set; }
    public string? HangfireJobId { get; set; }

    /// <summary>
    /// Will be populated with the CRON expression if this is a recurring job.
    /// </summary>
    public string? Schedule { get; set; }
    public FutureJobStatus Status { get; set; }
    public string? ErrorMessage { get; set; }
}
