using Microsoft.Extensions.DependencyInjection;
using ProperCleanArchitecture.Application.Shared.Interfaces;
using ProperCleanArchitecture.Application.Shared.Services;

namespace ProperCleanArchitecture.Application.Product;

public static class SharedDependencyInjection
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        services.AddTransient<IDateService, DateService>();
        return services;
    }
}