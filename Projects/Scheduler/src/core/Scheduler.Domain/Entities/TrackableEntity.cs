namespace Scheduler.Domain.Entities;

public abstract class TrackableEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DisabledAt { get; set; }
}
