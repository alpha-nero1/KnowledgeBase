using Scheduler.Core.Enums;

namespace Scheduler.Domain.Entities;

public class ScheduledTask
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public JobType Type { get; set; }
    public DateTime ExecuteAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? HangfireJobId { get; set; }
    public TaskStatus Status { get; set; }
    public string? ErrorMessage { get; set; }
}

public enum TaskStatus
{
    Pending,
    Scheduled,
    Processing,
    Completed,
    Failed
}