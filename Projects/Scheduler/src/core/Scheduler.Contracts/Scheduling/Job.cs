using Scheduler.Core.Enums;

namespace Scheduler.Contracts.Scheduling;

public class Job
{
    public int OrderId { get; set; }
    public JobType Type { get; set; }
    public DateTime ExecuteAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
