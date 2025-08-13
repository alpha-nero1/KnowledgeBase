using Scheduler.Core.Enums;

namespace Scheduler.Contracts.Scheduling;

/// <summary>
/// A Job that will be executed in the future via our Scheduling mechanism.
/// </summary>
public class FutureJobDto : TrackableDto
{
    public int FutureJobId { get; set; }
    public int OrderId { get; set; }
    public FutureJobType Type { get; set; }
    
    /// <summary>
    /// Will be populated with the CRON expression if this is a recurring job.
    /// </summary>
    public string? Schedule { get; set; }
    public FutureJobStatus Status { get; set; }
    public DateTime ExecuteAt { get; set; }
    public string? ErrorMessage { get; set; }
}
