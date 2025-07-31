using Microsoft.EntityFrameworkCore;
using Scheduler.Application.Scheduling.Interfaces;
using Scheduler.Domain.Entities;
using Scheduler.Infrastructure.Persistence;
using Scheduler.Core.Enums;

namespace Scheduler.Infrastructure.Scheduling.Repositories;

public class FutureJobRepository : IFutureJobRepository
{
    private readonly AppDbContext _context;

    public FutureJobRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FutureJob> AddAsync(FutureJob FutureJob)
    {
        _context.FutureJobs.Add(FutureJob);
        await _context.SaveChangesAsync();
        return FutureJob;
    }

    public async Task<FutureJob?> GetByIdAsync(int id)
    {
        return await _context.FutureJobs.FindAsync(id);
    }

    public async Task<FutureJob?> GetByHangfireJobIdAsync(string hangfireJobId)
    {
        return await _context.FutureJobs
            .FirstOrDefaultAsync(t => t.HangfireJobId == hangfireJobId);
    }

    public async Task<IEnumerable<FutureJob>> GetPendingTasksAsync()
    {
        return await _context.FutureJobs
            .Where(t => t.Status == FutureJobStatus.Pending || t.Status == FutureJobStatus.Scheduled)
            .OrderBy(t => t.ExecuteAt)
            .ToListAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var task = await _context.FutureJobs.FindAsync(id);
        if (task != null)
        {
            _context.FutureJobs.Remove(task);
            await _context.SaveChangesAsync();
        }
    }

    public Task<IEnumerable<FutureJob>> GetByOrderIdAsync(int orderId)
    {
        throw new NotImplementedException();
    }

    public async Task<FutureJob> UpdateAsync(FutureJob FutureJob)
    {
        _context.FutureJobs.Update(FutureJob);
        await _context.SaveChangesAsync();
        return FutureJob;
    }
}
