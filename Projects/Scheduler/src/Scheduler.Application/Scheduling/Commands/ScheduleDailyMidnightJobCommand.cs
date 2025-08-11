using MediatR;

namespace Scheduler.Application.Scheduling.Commands;

/// <summary>
/// Command to schedule a daily job that runs at midnight UTC.
/// </summary>
public record ScheduleDailyMidnightJobCommand(
    string JobId,
    int OrderId
) : IRequest<string>;
