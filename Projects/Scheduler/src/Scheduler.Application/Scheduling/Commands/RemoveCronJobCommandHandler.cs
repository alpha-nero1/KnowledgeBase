using MediatR;
using Scheduler.Application.Scheduling.Interfaces;

namespace Scheduler.Application.Scheduling.Commands;

/// <summary>
/// Handler for removing CRON jobs.
/// </summary>
public class RemoveCronJobCommandHandler(ICronSchedulerService _cronSchedulerService)
    : IRequestHandler<RemoveCronJobCommand>
{
    public Task Handle(RemoveCronJobCommand request, CancellationToken cancellationToken)
    {
        _cronSchedulerService.RemoveRecurringJob(request.JobId);
        return Task.CompletedTask;
    }
}
