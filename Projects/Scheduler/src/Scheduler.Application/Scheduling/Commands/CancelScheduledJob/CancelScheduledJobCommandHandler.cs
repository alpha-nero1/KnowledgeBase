using MediatR;
using Scheduler.Application.Scheduling.Interfaces;

namespace Scheduler.Application.Scheduling.Commands;

/// <summary>
/// Handler for removing CRON jobs.
/// </summary>
public class CancelScheduledJobHandler(ISchedulerService _schedulerService)
    : IRequestHandler<CancelScheduledJobCommand>
{
    public async Task Handle(CancelScheduledJobCommand request, CancellationToken cancellationToken)
    {
        await _schedulerService.CancelJobAsync(request.FutureJobId);
    }
}
