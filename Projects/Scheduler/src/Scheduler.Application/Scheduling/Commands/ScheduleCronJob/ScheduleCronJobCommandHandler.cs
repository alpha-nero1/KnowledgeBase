using MediatR;
using Scheduler.Application.Scheduling.Interfaces;

namespace Scheduler.Application.Scheduling.Commands.ScheduleCronJob;

/// <summary>
/// Handler for scheduling CRON jobs.
/// </summary>
public class ScheduleCronJobCommandHandler(ISchedulerService _schedulerService) 
    : IRequestHandler<ScheduleCronJobCommand, int>
{
    public async Task<int> Handle(ScheduleCronJobCommand request, CancellationToken cancellationToken)
    {
        var job = await _schedulerService.ScheduleRecurringJob(
            request.OrderId, 
            request.Type,
            request.CronExpression);
        return job.FutureJobId;
    }
}
