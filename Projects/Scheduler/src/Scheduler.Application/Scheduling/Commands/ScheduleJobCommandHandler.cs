using MediatR;
using Scheduler.Application.Scheduling.Interfaces;

namespace Scheduler.Application.Scheduling.Commands;

public class ScheduleJobCommandHandler : IRequestHandler<ScheduleJobCommand, int>
{
    private readonly ISchedulerService _schedulerService;

    public ScheduleJobCommandHandler(ISchedulerService schedulerService)
    {
        _schedulerService = schedulerService;
    }

    public async Task<int> Handle(ScheduleJobCommand request, CancellationToken cancellationToken)
    {
        var job = await _schedulerService.ScheduleJobAsync(request.OrderId, request.Type, request.ExecuteAt);
        return job.FutureJobId;
    }
}
