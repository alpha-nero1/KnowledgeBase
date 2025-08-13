using MediatR;

namespace Scheduler.Application.Scheduling.Commands;

/// <summary>
/// Command to remove a recurring CRON job.
/// </summary>
public record CancelScheduledJobCommand(int FutureJobId) : IRequest;
