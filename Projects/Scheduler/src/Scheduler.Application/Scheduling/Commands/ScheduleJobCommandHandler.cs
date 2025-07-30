using MediatR;
using Scheduler.Application.Scheduling.Interfaces;

namespace Scheduler.Application.Scheduling.Commands;

public class ScheduleJobCommandHandler : IRequestHandler<ScheduleJobCommand, string>
{
    private readonly ISchedulerService _schedulerService;

    public ScheduleJobCommandHandler(ISchedulerService schedulerService)
    {
        _schedulerService = schedulerService;
    }

    public async Task<string> Handle(ScheduleJobCommand request, CancellationToken cancellationToken)
    {
        var jobId = await _schedulerService.ScheduleJobAsync(request.Job);
        return jobId;
    }
}
