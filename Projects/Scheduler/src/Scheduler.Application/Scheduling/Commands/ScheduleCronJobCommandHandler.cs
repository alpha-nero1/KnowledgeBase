using MediatR;
using Scheduler.Application.Scheduling.Interfaces;

namespace Scheduler.Application.Scheduling.Commands;

/// <summary>
/// Handler for scheduling CRON jobs.
/// </summary>
public class ScheduleCronJobCommandHandler(ICronSchedulerService _cronSchedulerService) 
    : IRequestHandler<ScheduleCronJobCommand, string>
{
    public Task<string> Handle(ScheduleCronJobCommand request, CancellationToken cancellationToken)
    {
        var jobId = _cronSchedulerService.ScheduleRecurringJob(
            request.JobId, 
            request.CronExpression, 
            request.OrderId);
        
        return Task.FromResult(jobId);
    }
}
