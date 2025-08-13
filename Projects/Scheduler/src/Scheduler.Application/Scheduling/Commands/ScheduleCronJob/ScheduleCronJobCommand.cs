using MediatR;
using Scheduler.Core.Enums;

namespace Scheduler.Application.Scheduling.Commands.ScheduleCronJob;

/// <summary>
/// Command to schedule a recurring CRON job.
/// </summary>
public record ScheduleCronJobCommand(
    int OrderId, 
    FutureJobType Type,
    string CronExpression
) : IRequest<int>;
