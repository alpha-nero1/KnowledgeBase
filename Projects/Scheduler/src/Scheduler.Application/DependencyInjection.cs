using Microsoft.Extensions.DependencyInjection;
using Scheduler.Application.Scheduling.Interfaces;
using Scheduler.Application.Scheduling.Queries;
using System.Reflection;

namespace Scheduler.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationDependencyInjection).Assembly));
        
        // Register all IFutureJobExecutor implementations using Scan
        services.Scan(scan => scan
            .FromAssemblyOf<GetFutureJobQuery>()
            .AddClasses(classes => classes.AssignableTo<IFutureJobExecutor>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}