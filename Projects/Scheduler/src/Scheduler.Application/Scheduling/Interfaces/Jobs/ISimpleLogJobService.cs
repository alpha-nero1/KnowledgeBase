namespace Scheduler.Application.Scheduling.Interfaces.Jobs;

public interface ISimpleLogJobService
{
    public Task ExecuteAsync(string message);    
}