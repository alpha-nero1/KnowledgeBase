using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using Scheduler.Infrastructure.Persistence;
using Scheduler.Infrastructure.Scheduling.Services;
using Scheduler.Infrastructure.Scheduling.Repositories;
using Scheduler.Application.Scheduling.Interfaces;
using Hangfire.Storage.SQLite;

namespace Scheduler.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration,
        bool isApi = false
    )
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection")!;
        // Register DbContext with SQLite
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

        // Register Hangfire (simplified configuration)
        // Configure Hangfire with SQLite storage (shared between API and Worker)
        services.AddHangfire(config => 
        {
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                  .UseSimpleAssemblyNameTypeSerializer()
                  .UseRecommendedSerializerSettings()
                  .UseSQLiteStorage(connectionString);
        });

        // Only add hangfire server if not in API mode!
        if (!isApi)
        {
            services.AddHangfireServer(options =>
            {
                options.ServerName = "Worker-Server";
                options.WorkerCount = Environment.ProcessorCount;
            });
        }

        services.AddScoped<IFutureJobRepository, FutureJobRepository>();
        services.AddScoped<ISchedulerService, SchedulerService>();

        return services;
    }
}