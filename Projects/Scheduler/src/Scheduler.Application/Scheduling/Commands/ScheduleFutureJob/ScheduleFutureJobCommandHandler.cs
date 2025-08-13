using MediatR;
using Scheduler.Application.Scheduling.Interfaces;

namespace Scheduler.Application.Scheduling.Commands.ScheduleFutureJob;

public class ScheduleFutureJobCommandHandler(ISchedulerService _schedulerService) : IRequestHandler<ScheduleFutureJobCommand, int>
{
    public async Task<int> Handle(ScheduleFutureJobCommand request, CancellationToken cancellationToken)
    {
        var job = await _schedulerService.ScheduleJobAsync(request.OrderId, request.Type, request.ExecuteAt);
        return job.FutureJobId;
    }
}
