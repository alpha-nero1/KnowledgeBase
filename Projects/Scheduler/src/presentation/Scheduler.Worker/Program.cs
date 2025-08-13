using Scheduler.Application;
using Scheduler.Infrastructure;
using Scheduler.Worker;

var builder = Host.CreateApplicationBuilder(args);
// Hey pal, don't forget to add logging!
builder.Services.AddLogging(configure =>
    {
        configure.AddConsole(); 
        configure.AddDebug();
    });
var services = builder.Services;
var configuration = builder.Configuration;

services.AddApplication();
services.AddInfrastructure(configuration, false);

services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
