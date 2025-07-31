using MediatR;
using Scheduler.Core.Enums;

namespace Scheduler.Application.Scheduling.Commands;

/// <summary>
/// Schedule a job, return FutureJobId.
/// </summary>
public record ScheduleJobCommand(
    int OrderId, 
    FutureJobType Type, 
    DateTime ExecuteAt
) : IRequest<int>;
