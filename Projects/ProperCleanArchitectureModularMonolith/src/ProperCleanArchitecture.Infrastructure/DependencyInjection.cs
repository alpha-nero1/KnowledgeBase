using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProperCleanArchitecture.Application.Order.Interfaces;
using ProperCleanArchitecture.Application.Product.Interfaces;
using ProperCleanArchitecture.Infrastructure.Data;
using ProperCleanArchitecture.Infrastructure.Order.Repositories;
using ProperCleanArchitecture.Infrastructure.Products;

namespace ProperCleanArchitecture.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(IServiceCollection services, IConfiguration configuration)
    {
        // Database configuration
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Data Source=ProperCleanArchitecture.db";

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

        // Configure database settings
        services.Configure<DatabaseSettings>(
            configuration.GetSection(DatabaseSettings.SectionName));

        // Repository registrations
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}
