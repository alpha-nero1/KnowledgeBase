using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using worker;

var builder = Host.CreateApplicationBuilder(args);
var services = builder.Services;
services.AddHostedService<Worker>();
services.AddTransient<IMessageHandler, MessageHandler>();

var host = builder.Build();
host.Run();
