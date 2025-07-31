using Scheduler.Core.Enums;

namespace Scheduler.Domain.Entities;

public class FutureJob : TrackableEntity
{
    public int FutureJobId { get; set; }
    public int OrderId { get; set; }
    public FutureJobType Type { get; set; }
    public DateTime ExecuteAt { get; set; }
    public string? HangfireJobId { get; set; }
    public FutureJobStatus Status { get; set; }
    public string? ErrorMessage { get; set; }
}
