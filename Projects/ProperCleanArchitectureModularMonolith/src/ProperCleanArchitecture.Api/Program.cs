using ProperCleanArchitecture.Infrastructure;
using ProperCleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

#region DI
var services = builder.Services;
var config = builder.Configuration;

services.AddControllers();
ApplicationDependencyInjection.AddApplication(services);
InfrastructureDependencyInjection.AddInfrastructure(services, config);

#endregion

#region Build app
var app = builder.Build();

// Auto-migrate and optionally seed database based on configuration
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var databaseSettings = scope.ServiceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value;

    if (databaseSettings.EnableAutoMigration)
    {
        await dbContext.Database.MigrateAsync();
        app.Logger.LogInformation("Database migration completed.");
    }

    if (databaseSettings.EnableSeeding)
    {
        await DatabaseSeeder.SeedAsync(dbContext);
        app.Logger.LogInformation("Database seeding completed.");
    }
    else
    {
        app.Logger.LogInformation("Database seeding is disabled. Set Database:EnableSeeding to true in appsettings to enable.");
    }
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
#endregion;
