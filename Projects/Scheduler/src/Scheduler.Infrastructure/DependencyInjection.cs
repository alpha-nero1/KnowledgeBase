using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using Scheduler.Infrastructure.Persistence;
using Scheduler.Infrastructure.Scheduling.Services;
using Scheduler.Infrastructure.Scheduling.Repositories;
using Scheduler.Application.Scheduling.Interfaces;

namespace Scheduler.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Register DbContext with SQLite
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        // Register Hangfire (simplified configuration)
        services.AddHangfire(config => config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180));
        services.AddHangfireServer();

        services.AddScoped<IFutureJobRepository, FutureJobRepository>();
        services.AddScoped<ISchedulerService, SchedulerService>();

        return services;
    }
}