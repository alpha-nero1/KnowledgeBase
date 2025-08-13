using MediatR;
using Scheduler.Core.Enums;

namespace Scheduler.Application.Scheduling.Commands.ScheduleFutureJob;

/// <summary>
/// Schedule a job, return FutureJobId.
/// </summary>
public record ScheduleFutureJobCommand(
    int OrderId, 
    FutureJobType Type, 
    DateTime ExecuteAt
) : IRequest<int>;
