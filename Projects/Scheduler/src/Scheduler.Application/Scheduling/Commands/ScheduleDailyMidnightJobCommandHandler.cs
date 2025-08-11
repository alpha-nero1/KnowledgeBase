using MediatR;
using Scheduler.Application.Scheduling.Interfaces;

namespace Scheduler.Application.Scheduling.Commands;

/// <summary>
/// Handler for scheduling daily midnight jobs.
/// </summary>
public class ScheduleDailyMidnightJobCommandHandler(ICronSchedulerService _cronSchedulerService)
    : IRequestHandler<ScheduleDailyMidnightJobCommand, string>
{
    public Task<string> Handle(ScheduleDailyMidnightJobCommand request, CancellationToken cancellationToken)
    {
        var jobId = _cronSchedulerService.ScheduleDailyMidnightJob(request.JobId, request.OrderId);
        return Task.FromResult(jobId);
    }
}
