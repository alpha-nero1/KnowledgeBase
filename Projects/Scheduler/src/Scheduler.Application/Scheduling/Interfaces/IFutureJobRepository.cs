using Scheduler.Domain.Entities;

namespace Scheduler.Application.Scheduling.Interfaces;

/// <summary>
/// Repository for managing future job entities.
/// </summary>
public interface IFutureJobRepository
{
    Task<FutureJob> AddAsync(FutureJob scheduledTask);
    Task<FutureJob?> GetByIdAsync(int id);
    Task<IEnumerable<FutureJob>> GetByOrderIdAsync(int orderId);
    Task<IEnumerable<FutureJob>> GetPendingTasksAsync();
    Task<FutureJob> UpdateAsync(FutureJob scheduledTask);
    Task DeleteAsync(int id);
}
