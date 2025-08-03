using Scheduler.Application;
using Scheduler.Infrastructure;
using Scheduler.Worker;

var builder = Host.CreateApplicationBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddApplication();
services.AddInfrastructure(configuration, false);

services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
