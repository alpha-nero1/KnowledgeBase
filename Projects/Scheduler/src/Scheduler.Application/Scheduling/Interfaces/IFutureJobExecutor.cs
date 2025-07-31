using Scheduler.Contracts.Scheduling;
using Scheduler.Core.Enums;

namespace Scheduler.Application.Scheduling.Interfaces;

public interface IFutureJobExecutor
{
    public FutureJobType Type { get; }

    public Task ExecuteAsync(FutureJobDto futureJob);
}