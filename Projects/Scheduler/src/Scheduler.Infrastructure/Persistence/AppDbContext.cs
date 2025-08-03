using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Entities;

namespace Scheduler.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<FutureJob> FutureJobs { get; set; } = null!;
}
