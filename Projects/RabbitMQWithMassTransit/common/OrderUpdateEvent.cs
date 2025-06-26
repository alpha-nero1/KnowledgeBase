namespace common;

public enum OrderStatus
{
    Error = -1,
    NotStarted = 0,
    InProgress = 1,
    Complete = 2
}

public record OrderUpdateEvent(
    Guid Id,
    int OrderId,
    OrderStatus OrderStatus,
    DateTime Timestamp
);
