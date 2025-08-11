using MediatR;

namespace Scheduler.Application.Scheduling.Commands;

/// <summary>
/// Command to schedule a recurring CRON job.
/// </summary>
public record ScheduleCronJobCommand(
    string JobId,
    string CronExpression,
    int OrderId
) : IRequest<string>;
