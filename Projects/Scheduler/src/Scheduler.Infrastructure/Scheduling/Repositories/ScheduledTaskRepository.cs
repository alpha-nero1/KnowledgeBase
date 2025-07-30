using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Entities;
using Scheduler.Application.Scheduling.Interfaces;
using Scheduler.Infrastructure.Persistence;
using DomainTaskStatus = Scheduler.Domain.Entities.TaskStatus;

namespace Scheduler.Infrastructure.Scheduling.Repositories;

public class ScheduledTaskRepository : IScheduledTaskRepository
{
    private readonly SchedulerDbContext _context;

    public ScheduledTaskRepository(SchedulerDbContext context)
    {
        _context = context;
    }

    public async Task<ScheduledTask> AddAsync(ScheduledTask scheduledTask)
    {
        _context.ScheduledTasks.Add(scheduledTask);
        await _context.SaveChangesAsync();
        return scheduledTask;
    }

    public async Task<ScheduledTask?> GetByIdAsync(int id)
    {
        return await _context.ScheduledTasks.FindAsync(id);
    }

    public async Task<ScheduledTask?> GetByHangfireJobIdAsync(string hangfireJobId)
    {
        return await _context.ScheduledTasks
            .FirstOrDefaultAsync(t => t.HangfireJobId == hangfireJobId);
    }

    public async Task<IEnumerable<ScheduledTask>> GetPendingTasksAsync()
    {
        return await _context.ScheduledTasks
            .Where(t => t.Status == DomainTaskStatus.Pending || t.Status == DomainTaskStatus.Scheduled)
            .OrderBy(t => t.ExecuteAt)
            .ToListAsync();
    }


    public async Task DeleteAsync(int id)
    {
        var task = await _context.ScheduledTasks.FindAsync(id);
        if (task != null)
        {
            _context.ScheduledTasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }

    public Task<IEnumerable<ScheduledTask>> GetByOrderIdAsync(int orderId)
    {
        throw new NotImplementedException();
    }

    public async Task<ScheduledTask> UpdateAsync(ScheduledTask scheduledTask)
    {
        _context.ScheduledTasks.Update(scheduledTask);
        await _context.SaveChangesAsync();
        return scheduledTask;
    }
}
